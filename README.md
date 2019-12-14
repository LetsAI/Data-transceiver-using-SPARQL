# Data-transceiver-using-SPARQL
This is an implementation of how to transfer and manipulate data between the Arduino chip and the c-sharp program using the serial port and SPARQL.
  
[**SPARQL**](https://en.wikipedia.org/wiki/SPARQL) is an RDF query language—that is, a semantic query language for databases—able to retrieve and manipulate data stored in Resource Description Framework format.
  
The tasks used here are:
* Receives all five parameters values from Arduino
* sends SPARQL-queries to KB and answers to Arduino
* C# app shows all received parameters values and shows, which exactly of them have caused the alarm or warning
