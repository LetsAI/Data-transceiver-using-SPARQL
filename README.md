# Data-transceiver-using-SPARQL
  This is an implementation of how to transfer and manipulate data between the Arduino chip and the c-sharp program using the serial port   and SPARQL.<br/><br/>

### Contents
* [Description](#description)
* [Tasks](#tasks)
* [Results](#results)
* [
### Description   
  [**SPARQL**](https://en.wikipedia.org/wiki/SPARQL) is an RDF query language—that is, a semantic query language for databases—able to       retrieve and manipulate data stored in Resource Description Framework format.
  
The tasks used here are:
* Receives all five parameters values from Arduino
* Sends SPARQL-queries to KB and answers to Arduino
* C# app shows all received parameters values and shows, which exactly of them have caused the alarm or warning
* Sends to Arduino the answer, that it’s Alarm or Warning situation
* Arduino should turn on red (Alarm) or yellow (Warning) LED
* A button when pressed, it will find and replace the old values of triple nodes (delete them using “graph.Retract(...)” command), then create new triple and add this to graph (“graph.Assert(...)”).

