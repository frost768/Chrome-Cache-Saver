#-*-coding:utf8;-*-
#qpy:3
#qpy:console
import os, shutil, re ,binascii, gzip
d = f"{os.environ.get('LOCALAPPDATA')}\BraveSoftware\Brave-Browser\\User Data\Default\\Cache\\"
s = f"{os.environ.get('USERPROFILE')}\\Desktop\\FileCache\\"
gifs = []
pngs = []
jpgs = []
mpgs = []
gzips = []
lav =[]
odd = []
byt = []
def clean():
 if os.path.exists(s):
  print("\nÇalışma dizini temizleniyor...\n")
  a=os.listdir(s)
  for i in a:
   if os.path.isfile(s+i) ==True:
    os.remove(s+i)
  a = os.listdir(s)
  for i in a: 
   if os.path.isfile(s+i)==False:
    x = os.listdir(s+i)
    for q in x:
     os.remove(s+i+"/"+q)
    os.rmdir(s+i)
a=os.listdir(d)
a.remove("data_0")
a.remove("data_1")
a.remove("data_2")
a.remove("data_3")
a.remove("index")

def cop(y):
 print("Cache kopyalanıyor...")
 if os.path.exists(s):
    for i in a:
      if os.path.getsize(d+i)> int(y)*1024:
        shutil.copy(d+i, s+i)
 else:
    os.mkdir(s)
    for i in a:
      if os.path.getsize(d+i)> int(y)*1024:
        shutil.copy(d+i, s+i)



def find(y): 
 print("Dosya türleri bulunuyor...\n")
 fi = os.listdir(s)
 for i in fi:
   if os.path.isfile(s+i) == True:
    with open(s+i,"rb") as f:
     z = f.read(10)
     if re.search("JFIF", str(z)) == None and not re.search("Lav",str(z))==None:
      lav.append(i)

     if not re.search("GIF89|GIF87",str(z))==None:
      gifs.append(i)

     if not re.search("PNG", str(z)) == None:
      pngs.append(i)

     if not re.search("JFIF", str(z)) == None or not re.search("Exif", str(z)) == None:
      jpgs.append(i)

     if not re.search('FFmpeg', str(z)) == None or not re.search('ID3', str(z)) == None:
      mpgs.append(i)

     if not re.search('1f8b0', str(binascii.hexlify(z))) == None:
       gzips.append(i)

def find_gzip():
 print("Dosya türleri bulunuyor...\n")
 fi = os.listdir(s+"gzips")
 for i in fi:
   if os.path.isfile(s+"gzips\\"+i) == True:
    with open(s+"gzips\\"+i,"rb") as f:
     z = f.read(10)
     if re.search("JFIF", str(z)) == None and not re.search("Lav",str(z))==None:
      lav.append(i)

     if not re.search("GIF89|GIF87",str(z))==None:
      gifs.append(i)

     if not re.search("PNG", str(z)) == None:
      pngs.append(i)

     if not re.search("JFIF", str(z)) == None or not re.search("Exif", str(z)) == None:
      jpgs.append(i)

     if not re.search('FFmpeg', str(z)) == None or not re.search('ID3', str(z)) == None:
      mpgs.append(i)
             
def move():
 if not len(gifs)==0:
  if not os.path.exists(s+"gifs"):
   os.mkdir(s+"gifs")
  print("Gifler taşınıyor...\n")
  for g in gifs:
   shutil.move(s+g, s+"gifs/"+g+".gif")

 if not len(pngs) ==0:
  if not os.path.exists(s+"pngs"):
   os.mkdir(s+"pngs")
  print("Pngler  taşınıyor...\n")
  for p in pngs:
   shutil.move(s+p, s+"pngs/"+p+".png")
  
 if not len(jpgs)==0:
  if not os.path.exists(s+"jpgs"):
   os.mkdir(s+"jpgs")
  print("JPEG'ler taşınıyor...\n")
  for j in jpgs:
   shutil.move(s+j, s+"jpgs/"+j+".jpeg")
 
 if not len(lav)==0:
  if not os.path.exists(s+"lav"):
   os.mkdir(s+"lav")
  for w in lav:
   shutil.move(s+w, s+"lav/"+w+".jpeg")

 if not len(mpgs)==0:
  if not os.path.exists(s+"mpgs"):
   os.mkdir(s+"mpgs")
  for h in mpgs:
   shutil.move(s+h, s+"mpgs/"+h+".mpeg")
   
 if not len(gzips)==0:
  if not os.path.exists(s+"gzips"):
   os.mkdir(s+"gzips")
  for z in gzips:
   shutil.move(s+z, s+"gzips/"+z+".gzip")
   out=open(s+"gzips/"+z,"wb")
   out.write(gzip.open(s+"gzips/"+z+".gzip","rb").read())
   out.close()
   os.remove(s+"gzips/"+z+".gzip")

def move_gzip():
 if not len(gifs)==0:
  print("Gifler taşınıyor...\n")
  for g in gifs:
   shutil.move(s+"gzips\\"+g, s+"gifs/"+g+".gif")

 if not len(pngs) ==0:
  print("Pngler  taşınıyor...\n")
  for p in pngs:
   shutil.move(s+"gzips\\"+p, s+"pngs/"+p+".png")
  
 if not len(jpgs)==0:
  print("JPEG'ler taşınıyor...\n")
  for j in jpgs:
   shutil.move(s+"gzips\\"+j, s+"jpgs/"+j+".jpeg")
 
 if not len(lav)==0:
  for w in lav:
   shutil.move(s+"gzips\\"+w, s+"lav/"+w+".jpeg")

 if not len(mpgs)==0:
  for h in mpgs:
   shutil.move(s+"gzips\\"+h, s+"mpgs/"+h+".mpeg")

def fix():
 print("Dosyalar düzeltiliyor...\n")
 if not len(gifs) ==0:
  k = os.listdir(s+"gifs/")
  for i in k:
   with open(s+"gifs/"+i, "r+b") as f:
    t = str(binascii.hexlify(f.read())).split("474946")[1].split("'")
    h = "474946"+t[0]
    if len(h)%2!=0:
     h =h+"0"
     odd.append(i)
     byt.append((t[0])[len(t[0])-5:len(t[0])])
   with open(s+"gifs/"+i,"wb") as d:
    d.write(binascii.unhexlify(bytes(h,"ascii")))
 
 if not len(pngs)==0:
  m = os.listdir(s+"pngs/")
  for i in m:
   with open(s+"pngs/"+i, "r+b") as f:
    x = f.read()
    y = binascii.hexlify(x)
    z = str(y).split("504e47")
    t = z[1].split("'")
    h = "89504e47"+t[0]
    if len(h)%2!=0:
     h =h+"0"
     byt.append(t[0][len(t[0])-5:len(t[0])])
     odd.append(i)
   with open(s+"pngs/"+i,"wb") as d:
    d.write(binascii.unhexlify(bytes(h,"ascii")))
 
 if not len(jpgs)==0:
  r = os.listdir(s+"jpgs/")
  for i in r:
   with open(s+"jpgs/"+i, "r+b") as f:
    x = f.read()
    y = binascii.hexlify(x)
    z = str(y).split("4a464946")
    if len(z)==3:
     t = z[2].split("'")
     h = "ffd8ffe0001045786966"+z[1]+"ffd8ffe0001045786966"+t[0]
    else:
     t = z[1].split("'")
     h = "ffd8ffe0001045786966"+t[0]
    if len(h)%2!=0:
     h =h+"0"
     odd.append(i)
     byt.append((t[0])[len(t[0])-5:len(t[0])])
    with open(s+"jpgs/"+i,"wb") as d:
     d.write(binascii.unhexlify(bytes(h,"ascii")))
 
 if not len(lav)==0:
  y = os.listdir(s+"lav/")
  for i in y:
   with open(s+"lav/"+i, "r+b") as f:
    x = f.read()
    y = binascii.hexlify(x)
    z = str(y).split("4c617663")
    t = z[1].split("'")
    h = "ffd8ffe000104c617663"+t[0]
    if len(h)%2!=0:
     h =h+"0"
     odd.append(i)
     byt.append((t[0])[len(t[0])-5:len(t[0])])
   with open(s+"lav/"+i,"wb") as d:
    d.write(binascii.unhexlify(bytes(h,"ascii")))
   if not os.path.exists(s+"jpgs"):
    os.mkdir(s+"jpgs")
    
   shutil.move(s+"lav/"+i, s+"jpgs/"+i)
  os.rmdir(s+"lav")

def clean2():
 print("Temizleniyor...\n")
 j = os.listdir(s)
 for i in j:
  if os.path.isfile(s+i)==True:
   os.remove(s+i)
 j = os.listdir(s+"gzips\\")
 for i in j:
  if os.path.isfile(s+"gzips\\"+i)==True:
   os.remove(s+"gzips\\"+i)
 print("Bütün dosyalar ayrıştırıldı!")

if __name__ == "__main__":
  y= input("Gireceğiniz KB değerinden küçük olan dosyalar silinecek:")
  clean()
  cop(y)
  find(y)
  move()
  lav=[]
  gifs=[]
  pngs=[]
  jpgs=[]
  mpgs=[]
  find_gzip()
  move_gzip()
  #fix()
  clean2()
  print(pngs)
  print(gifs)
  print(jpgs)
  if not len(odd)==0:
   for i in odd:
    for n in byt:
     print("Düzenlenen dosya:"+i+" Son 5 byte:"+n)
