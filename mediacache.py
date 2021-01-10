"""
A script for extracting video files from media cache of Chromium based apps
Author : ChrNolan52
Last updated: Sun Jan 10 2021 16:22:52 GMT+0300 (GMT+03:00)
"""


import os, re, binascii
programs={
  "B":f"{os.environ.get('LOCALAPPDATA')}\BraveSoftware\Brave-Browser\\User Data\\Default\Cache\\",
  "C":f"{os.environ.get('LOCALAPPDATA')}\Google\Chrome\\User Data\Default\\Cache\\",
  "S":f"{os.environ.get('LOCALAPPDATA')}\Spotify\Browser\Cache\\",
  "D":f"{os.environ.get('APPDATA')}\discord\Cache"
}
cache = ""
output_folder = f"{os.environ.get('USERPROFILE')}\\Desktop\\Cache\\"
addr = []
def write(mp4s):
  print("No cache found") if len(mp4s)==0 else print(f"{len(mp4s)} videos found")
  if not os.path.exists(output_folder):
        os.mkdir(output_folder)
  for file in mp4s:
    file_path=f"{output_folder}{file[0]}.mp4"
    if not os.path.isfile(file_path):
       with open(file_path ,mode="wb") as mp4:
        for f in file:
          with open(cache + f,mode="rb") as fi:
            mp4.write(fi.read())
    else:
       os.remove(file_path)
       with open(file_path, mode="wb") as mp4:
        for f in file:
          with open(cache + f,mode="rb") as fi:
            mp4.write(fi.read())

def get_files():
  l=os.listdir(cache)
  l.remove("data_0")
  l.remove("data_1")
  l.remove("data_2")
  l.remove("data_3")
  l.remove("index")
  with open(cache+"\\data_1",mode="rb") as f:
    hexed = str(binascii.hexlify(f.read()))
  for i in l:
    f=i[2:]
    addr.append([f,f[4:6]+f[2:4]+f[0:2]+"80"])
  return hexed

def sort_urls(hexed):
    http_jpeg="68747470.{0,100}6a7067"
    http_mp4=".{84}68747470.{0,500}2e6d70343a.{28}3a.{0,6}"
    http_mp42=".{72}68747470.{0,500}2e6d7034"
    http_webm="68747470.{0,200}7765626d"
    jpeg=re.findall(http_jpeg,hexed)
    mp4=re.findall(http_mp4,hexed)
    mp42=re.findall(http_mp42,hexed)
    webm=re.findall(http_webm,hexed)
    
    mp4s=[]
    mp42s=[]
    urls = open(f"{output_folder}\\list.txt","w")
    for address in addr:
      for m in mp4:
        urls.write(str(binascii.unhexlify(m[84:-36]))[2:-1])
        if m[0:8] == address[1]:
          mp42s.append(["f_"+address[0],m[84:-36],int(str(binascii.unhexlify(m[-6:]))[2:-1].replace("\\x00",""),16)])
      for mp in mp42:
        urls.write(str(binascii.unhexlify(mp[84:]))[2:-1])
        if mp[0:8] == address[1]:
          mp4s.append(["f_"+address[0],mp[84:]])
    urls.close()
    mp42s=sorted(mp42s,key = lambda x:(x[1],x[2])) # x[1] = url, x[2] = order; sorts by url then order
    mp4s=sorted(mp4s,key = lambda x:x[1]) # x[1] = url ; sorts by url
    
    part_list=[]
    file_parts=[]
    for i in range(len(mp42s)-1):
      if mp42s[i][1] == mp42s[i+1][1]:
        file_parts.append(mp42s[i][0])
      else:
        file_parts.append(mp42s[i][0])
        part_list.append(file_parts)
        file_parts=[]
    
    file_parts=[] # reset list

    for i in range(len(mp4s)-1):
      if mp4s[i][1] == mp4s[i+1][1]:
        file_parts.append(mp4s[i][0])
      else:
        if len(file_parts)>1 :
          part_list.append(file_parts)
        file_parts=[]
    if len(file_parts)>1 :
      part_list.append(file_parts)

    write(part_list)

def main():
  i = input("\
  Please close the application before running the script \n \
  Leave empty for Chrome \n \
  [B]rave                \n \
  [S]potify              \n \
  [D]iscord              \n \
  [Q]uit                 \n \
  "
  )

  if i.upper() == "Q":
    return
  global cache
  cache = programs["C"] if i == "" else programs[i[0].upper()]
  if not os.path.exists(cache):
    print(f"{cache} folder not found")
    main()
  else:
    sort_urls(get_files())

if __name__ == "__main__":
  main()