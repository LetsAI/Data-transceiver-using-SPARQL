: int trig1 = 2;
int echo1 = 3;
//int distance1=0;

int trig2 = 4;
int echo2 = 5;
// distance2=0;

int trig3 = 6;
int echo3 = 7;
//int distance3=0;

int trig4 = 8;
int echo4 = 9;
//int distance4=0;

int Button = 10;
int echo5 = 11;
//int distance5=0;

int red1 = 12;
int yellow1 = 13 ;

int ButtonState = 0;

long duration,distance;


void setup() {
Serial.begin(9600);
pinMode(trig1,OUTPUT);
pinMode(echo1,INPUT);
pinMode(trig2,OUTPUT);
pinMode(echo2,INPUT);
pinMode(trig3,OUTPUT);
pinMode(echo3,INPUT);
pinMode(trig4,OUTPUT);
pinMode(echo4,INPUT);
pinMode(Button,INPUT);
// pinMode(trig5,INPUT);
pinMode(red1,OUTPUT);
pinMode(yellow1,OUTPUT);

}

void loop() {
int distance1 = getDistance(trig1, echo1);
Serial.print("Sensor1 = ");
Serial.println(distance1);
//leds(distance1);
//delay(100);

int distance2 = getDistance(trig2, echo2);
Serial.print("Sensor2 = ");
Serial.println(distance2);
//leds(distance2);
//delay(100);

int distance3 = getDistance(trig3, echo3);
Serial.print("Sensor3 = ");
Serial.println(distance3);
//leds(distance3);
//delay(100);
//
int distance4 = getDistance(trig4, echo4);
Serial.print("Sensor4 = ");
Serial.println(distance4);
//leds(distance4);
//delay(100);
//
ButtonState = digitalRead(Button);
if (ButtonState == HIGH)
{
Serial.println("Button");
}

//int distance5 = getDistance(trig5, echo5);
//Serial.print("Sensor5 = ");
//Serial.println(distance5);
////leds(distance5);
////delay(100);

//while (Serial. )
char data = Serial.read();
switch (data){

case '1': digitalWrite(red1,HIGH);
digitalWrite(yellow1,LOW);
break;

case '2': digitalWrite(yellow1,HIGH);
digitalWrite(red1,LOW);
break;

case '3': digitalWrite(yellow1,LOW);
digitalWrite(red1,LOW);
break;
}
}
int getDistance(int trig,int echo){
digitalWrite(trig,LOW);
delayMicroseconds(10);
digitalWrite(trig,HIGH);
delayMicroseconds(10);
digitalWrite(trig,LOW);
duration = pulseIn(echo,HIGH);
distance = (duration)/58;
return distance;
}
