import urllib.request
import json      

body = {'commands': ["forward","left","right"]}  

myurl = "http://192.168.0.116:5000/ship/json"
req = urllib.request.Request(myurl)
req.add_header('Content-Type', 'application/json; charset=utf-8')
jsondata = json.dumps(body)
jsondataasbytes = jsondata.encode('utf-8')   # needs to be bytes
req.add_header('Content-Length', len(jsondataasbytes))
print ("SUCCESS! You sent the JSON: " + jsondata)
response = urllib.request.urlopen(req, jsondataasbytes)
