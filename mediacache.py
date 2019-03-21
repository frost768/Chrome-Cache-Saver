#An application for extracting video files from media cache of Google Chrome
#ChrNolan52

import os, re, binascii, struct
#Defining variables and constants
d = r"C:\Users\SAMSUNG\AppData\Local\Google\Chrome\User Data\Profile 13\Cache\\"
#d = r"C:\Users\SAMSUNG\AppData\Local\Google\Chrome\User Data\Default\Cache\\"
s = r"C:\Users\SAMSUNG\Desktop\tmp2\\"
files = []
bins = []
addr = []
parts = []
urldict={}
hexdict={}
clean=[]
def get_files():
  l=os.listdir(d)
  l.remove("data_0")
  l.remove("data_1")
  l.remove("data_2")
  l.remove("data_3")
  l.remove("index")

  for i in l:
    files.append(i.split("f_")[1])

#Dosya isimlerini binary türüne çevirme
def conv2bin():
  for i in files:
    b=""
    for ix in range(0,6):
      b+=bin(int(i[ix],16))[2:].zfill(4)
    bins.append([i,"10000000"+b])

#Binary türü isimleri little endian a cevirme
def bin2le():
  for b in bins:
    little_endian=struct.pack("<L",int("80"+b[0],16))
    little_endian_b=str(little_endian).replace("\\x","").split("'")[1]
    #print("hex_string:"+ str(hex_string).replace("\\x","")+" little:"+str(little_endian).replace("\\x","").split("'")[1]+" len:"+str(len(str(little_endian).replace("\\x","").split("'")[1])))

    if len(little_endian_b)==8 and not ((little_endian_b[0]).encode("utf-8")).hex()=="5c":
      addr.append([b[0],little_endian_b])

    if len(little_endian_b)==7:
      if (little_endian_b)[6]=='"':
        addr.append([b[0],"27010080"])
      else:
        first_char=((little_endian_b[0]).encode("utf-8")).hex()
        addr.append([b[0],first_char+(little_endian_b)[1:]])
        
    if len(little_endian_b)==8 and ((little_endian_b[0]).encode("utf-8")).hex()=="5c":
      if little_endian_b[1]=='t':
         addr.append([b[0],"09"+(little_endian_b)[2:]])

      if little_endian_b[1]=='n':
         addr.append([b[0],"0a"+(little_endian_b)[2:]])
         
      if little_endian_b[1]=='r':
         addr.append([b[0],"0d"+(little_endian_b)[2:]])

      if little_endian_b[1]=='\\':
         addr.append([b[0],"5c"+(little_endian_b)[2:]])
       
#Data dosyasını okuma ve hex türüne cevirme
def read_data():
  with open(d+"\\data_1",mode="rb") as f:
    hexed = binascii.hexlify(f.read())
  return hexed

#Olusturulan adreslere gore url ve sıra alma
def sort_urls(hexed):
  for adres in addr:
      try:
        beginning=re.search(str(adres[1]),str(hexed)).start()
        http_flag=re.search("68747470",str(hexed[beginning:])).start()
        beginning=beginning+http_flag-2
        end=re.search("3a......0",str(hexed[beginning:])).start()
        hexed_url=hexed[beginning:beginning+end+6]
##    FOR DEBUG PURPOSES

##        print("adres: "+adres[1]+" beg: "+str(beginning)+" end: "+str(end)+" beg+end: "+str(beginning+end)+ \
##         "\n------------------------------------------------\n"+ \
##              str(binascii.unhexlify(hexed_url)).replace("\\x00","").split("'")[1] + \
##         "\n-----------------------------------------------")
        
        byte_data=str(hexed_url).split("'")[1][-6:]
        byte1=str(binascii.unhexlify(byte_data[0:2])).split("'")[1]
        byte2=str(binascii.unhexlify(byte_data[2:4])).split("'")[1]
        byte3=str(binascii.unhexlify(byte_data[4:6])).split("'")[1]
##        print("byte_data: "+byte_data+" ve "+byte1+" ve "+byte2+" ve "+byte3+"\n")
      except:
        print("hata "+ adres[1]+ byte_data[0]+" "+ str(beginning)+" "+ str(end)+ " bulunamadı")

      url_base=str(binascii.unhexlify(hexed_url)).replace("\\x00","").split("'")[1]
      if re.search("webm",url_base) is None:
        if byte2=="\\x00" and byte3=="\\x00":
          url = url_base[:-2]
          p=["f_"+adres[0],url,byte1]
        if byte3 == "\\x00" and byte2!="\\x00":
          url = url_base[:-3]
          p=["f_"+adres[0],url,byte1+byte2]
        if byte3 != "\\x00" and byte2!="\\x00":
          url = url_base[:-4]
          p=["f_"+adres[0],url,byte1+byte2+byte3]
        parts.append(p)


#URL gruplandirma
def categorize():
  for i in parts:
    for n in parts:
      if not n[1] == i[1] and not n[1] in clean:
        clean.append(n[1])
      else:
        clean.append(n[1])

  #gruplardan liste olusturma
  for i in clean:
    urldict[i] = []
    
  for url in urldict:
  #   gruplandirilmis url listelerine dosya parcalarinin indexlerini ekleme
      for member in range(0,len(parts)):
        if url==parts[member][1]:
          urldict[url].append(member)
      hexdict[url]=[]
      
  #   hexadecimal olarak belirtilen indexleri inte cevirme
      for partFileIndex in urldict[url]:
        if not len(urldict[url])==1:
          hexdict[url].append([partFileIndex,int(str(parts[partFileIndex][2]),16)])
  #   int haline gelen indexleri siralama
      hexdict[url]=sorted(hexdict[url],key = lambda x:int(x[1]))
  ##    FOR DEBUG PURPOSES
      if re.search("mp4",url):
        name=url.split(".mp4")[0][[(m.end()) for m in re.finditer("/",url.split(".mp4")[0])][-1]:].replace("\\","").replace("/","")
      elif re.search("mp3",url):
        name=url.split(".mp3")[0][[(m.end()) for m in re.finditer("/",url.split(".mp3")[0])][-1]:].replace("\\","").replace("/","")
      else:   
         name=url.split(".php")[0][[(m.end()) for m in re.finditer("/",url.split(".php")[0])][-1]:].replace("\\","").replace("/","")
      print("Video: "+name+"\nURL: "+url+"\n-----------------------------\nParts: ")
      b=1
      for i in hexdict[url]:
        if (b%7)==0:
          print(parts[i[0]][0])
        else:
          print(parts[i[0]][0],end=" ")
        b+=1
      print("\n")
        
##  Klasor olusturma
      if not os.path.exists(s):
        os.mkdir(s)
  ##  Dosyaları sırasıyla birleştirme
      if not os.path.isfile(s+name+".mp4"):
        with open(s+name+".mp4",mode="wb") as mp4:
          for i in hexdict[url]:
            with open(d+parts[i[0]][0],mode="rb") as file:
              mp4.write(file.read())
              
      elif not os.path.isfile(s+name+"(2).mp4"):
        with open(s+name+"(2).mp4",mode="wb") as mp4:
          for i in hexdict[url]:
            with open(d+parts[i[0]][0],mode="rb") as file:
              mp4.write(file.read())

      else:
        with open(s+name+"(3).mp4",mode="wb") as mp4:
          for i in hexdict[url]:
            with open(d+parts[i[0]][0],mode="rb") as file:
              mp4.write(file.read())
def clean_dir():
  a=os.listdir(s)
  for i in a:
    if os.path.getsize(s+i)==0:
      os.remove(s+i)

def calistir():
  get_files()
  conv2bin()
  bin2le()
  sort_urls(read_data())
  categorize()
  #clean_dir()
calistir()
input()