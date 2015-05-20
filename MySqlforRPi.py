import MySQLdb
import time
import serial
ser = serial.Serial('/dev/ttyACM0', 9600)

db = MySQLdb.connect(host = "thor.net.nait.ca", user = "jwu18000", 
					 passwd = "Th3r35dagger5", db="jwu18000_capstone")

cur = db.cursor()
cur.execute("SELECT ProductID FROM Products")	
rows = cur.fetchall()
rowList = list()
for r in rows:
	rowList.append(int(r[0]))
	
			
print "Welcome to InControl"
userInput = input("Are you Shipping or Receiving?Please enter S\R:  ")
	 
while 1:
	if userInput == "R":
		print "Please scan to Receive:"
		prodID = 0
		scanned = False;
		with db:					 
			search = db.cursor()
			update = db.cursor()
			prodID = int(ser.read())
			for products in rowList:
				if products == prodID and scanned == False:
					search.execute("SELECT ProductName From Products Where ProductID = %s" % prodID)
					for name in range(search.rowcount):
						pName = search.fetchone()
						print pName[0]
					update.execute("UPDATE Products Set QuantityInStock = QuantityInStock + 1, UnitsOnOrder = UnitsOnOrder - 1 Where ProductID = %s"% prodID)
					print "%s Updated"%pName[0]
                    ser.write("%s"%pName[0])
					scanned = True
					time.sleep(1)
	elif userInput == "S":
		print "Please scan to Ship:"
		prodID = 0
		scanned = False;
		with db:					 
			search = db.cursor()
			update = db.cursor()
			prodID = int(ser.read())
			for products in rowList:
				if products == prodID and scanned == False:
					search.execute("SELECT ProductName From Products Where ProductID = %s" % prodID)
					for name in range(search.rowcount):
						pName = search.fetchone()
						print pName[0]
					update.execute("UPDATE Products Set QuantityInStock = QuantityInStock - 1 Where ProductID = %s"% prodID)
					print "%s Updated"%pName[0]
                    ser.write("%s"%pName[0])
					scanned = True
					time.sleep(1)
	else:
		userInput = input("Are you Shipping or Receiving?Please enter S\R:  ")
			

			
		




		
	
	
	


	

