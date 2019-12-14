# Data-transceiver-using-SPARQL
  This is an implementation of how to transfer and manipulate data between the Arduino chip and the c-sharp program using the serial port and SPARQL.<br/>
<br/>

### Contents
* [Description](#Description)
* [Hardware requirements](#Hardware-requirements)
* [Tasks](#tasks)
* [C-map](#C-map)
* [Results](#Results)
<br/>

### Description
The basic idea is to receive values from the sensors and then send SPARQL-queries to the [Notation3 file](https://github.com/LetsAI/Data-transceiver-using-SPARQL/blob/master/C%23_code/bin/Debug/n3/N3.n3) which has the Knowledgebase to check whether which sensor has an alarm, warning or normal situation. After that, an order will be sent to Arduino to make an action.
<br/>
[**SPARQL**](https://en.wikipedia.org/wiki/SPARQL) is an RDF query language—that is, a semantic query language for databases—able to    retrieve and manipulate data stored in Resource Description Framework format.
<br/>

### Hardware requirements
* Arduino chip 
* Five sensors
* Leds 
* Resistance
* Button
<br/>

### Tasks
The tasks used here are:
* Receives all five parameters values from Arduino
* Sends SPARQL-queries to KB and answers to Arduino
* C# app shows all received parameters values and shows, which exactly of them have caused the alarm or warning
* Sends to Arduino the answer, that it’s Alarm or Warning situation
* Arduino should turn on red (Alarm) or yellow (Warning) LED
* A button when pressed, it will find and replace the old values of triple nodes (delete them using “graph.Retract(...)” command), then create new triple and add this to graph (“graph.Assert(...)”).
<br/>

### <a id="C-map">C-map</a>
The below images are representation for two situations alarm and warning using C-map:
* [Alarm](https://github.com/LetsAI/Data-transceiver-using-SPARQL/blob/master/Images/alarm.jpg)
* [Warning](https://github.com/LetsAI/Data-transceiver-using-SPARQL/blob/master/Images/warning.jpg)
<br/>

### Results
The below pictures represent the final results with C-Sharp GUI:
![](https://github.com/LetsAI/Data-transceiver-using-SPARQL/blob/master/Images/results1.jpg)
![](https://github.com/LetsAI/Data-transceiver-using-SPARQL/blob/master/Images/results2.jpg)

