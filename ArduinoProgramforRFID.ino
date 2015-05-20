#include <Adafruit_PN532.h>
#include <Wire.h>
#include <SPI.h>

#define PN532_SCK  (A0)
#define PN532_MOSI (A1)
#define PN532_SS   (A2)
#define PN532_MISO (A3)

Adafruit_PN532 nfc(PN532_SCK,PN532_MISO,PN532_MOSI,PN532_SS);
const byte maxChars = 16;
char ProductID[maxChars];
boolean entered = false;

void setup() {
  
  Serial.begin(115200);
  Serial.println("Welcome To InControl RFID Tracking");
  Serial.println("Looking for your PN532 Reader");
  nfc.begin();
  uint32_t versiondata = nfc.getFirmwareVersion();
  if(!versiondata)
  {
    Serial.println("PN532 board not found!");
    while(1);
  }
  nfc.SAMConfig();
  Serial.println("Ready to write to a Mifare Tag");
}

void loop() {
  uint8_t success;
  uint8_t uid[] = { 0,0,0,0,0,0,0};
  uint8_t uidLength;
  
  success = nfc.readPassiveTargetID(PN532_MIFARE_ISO14443A,uid,&uidLength);
  
  if(success){
    uint8_t keya[6] = {0xFF,0xFF,0xFF,0xFF,0xFF,0xFF};
    success = nfc.mifareclassic_AuthenticateBlock(uid,uidLength,4,0,keya);
    if(success)
    {    
      
      
      Serial.println("Ready to write");
      Serial.print("Will print to UID: "); nfc.PrintHex(uid,uidLength);
      delay(1000);
      while(!entered)
      {
          ProductIDInput();
          delay(5000);
      }
      ShowProductID();
      delay(1000);
      Serial.println("Input 'y' to proceed or 'n' to restart!");
      while(Serial.available() == 0){}
      char confirm = 'y';
      char decline = 'n';
      char input = Serial.read();
      if(input == confirm)
      {
        uint8_t data[16];
        for(int i = 0; i < 16; i++)
        {
          data[i] = ProductID[i]; 
        }
        
        success = nfc.mifareclassic_WriteDataBlock(4,data);
        if(success)
        {
          Serial.println("ProductID written successfully to Sector 1 (Blocks 4...7)");
          delay(500);
        }
        else
        {
          Serial.println("Error writting data");
          delay(500);
        }
      }
      else if(input == decline)
      {
        ProductIDInput();
        delay(5000);
      }
      else
      {
        Serial.println("Invalid input! Must be 'y' or 'n'!");
        delay(2500);
      }
      
      
         
    }
    else
    {
      Serial.println("Unable to read requested block, Try another tag");
    }
  }
  else
  {
    Serial.println("Unable to use card not a mifare tag"); 
  }
}

void ProductIDInput(){
  static boolean writing = false;
  static byte index = 0;
  char startMark = '<';
  char endMark = '>';
  char rc;

      Serial.print("");
      Serial.println("Please Input Product ID to upload to tag");
  while(Serial.available() > 0 && entered == false)
  {
    rc = Serial.read();
    if(writing == true)
    {
      if(rc != endMark)
      {
        ProductID[index] = rc;
        index++;
        if(index >= maxChars)
        {
          index = maxChars - 1;
        }
      }
      else
      {
        ProductID[index] = '\0';
        writing = false;
        index = 0;
        entered = true;
      }
    }
    else if(rc = startMark)
    {
      writing = true;
    }
  }
  
}

void ShowProductID()
{
  
  if(entered == true)
  {
    Serial.print("ProductID to be entered: "); 
    Serial.println(ProductID);
    entered = false;
  }

}

