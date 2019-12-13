using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using System.Collections;
using System.IO;
using System.IO.Ports;


namespace Lab
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();

        }


        private SparqlResultSet sparqlSend(string query)
        {
            var parser = new Notation3Parser();
            var graph = new Graph();
            parser.Load(graph, @"n3\\converter.n3");
            SparqlResultSet resultSet = graph.ExecuteQuery(query) as SparqlResultSet;

            return resultSet;
        }


        private string GenSparqlQuery(string AlarmVar,string WarningVar,string val)
        {
            string query = null;

            try
            {

                query =
@"PREFIX ns0: <http://www.semanticweb.org/laith/ontologies/2017/10/untitled-ontology-11#>
PREFIX my: <http://www.My-Wab31.org/ontologies/ont.owl>
SELECT ?state
WHERE {{" + Environment.NewLine;

                query += "\t" + AlarmVar + Environment.NewLine;
                query += "\t" + "ns0:state ?state;" + Environment.NewLine;
                query += "\t" + "ns0:Max ?max1;" + Environment.NewLine;
                query += "\t" + "ns0:Min ?min1]." + Environment.NewLine;
                query += "\t" + "FILTER (?min1 < "+ val +" && ?max1 > "+ val +").}" + Environment.NewLine;

                query += "UNION" + Environment.NewLine + "{" + Environment.NewLine; 

                query += "\t" + WarningVar + Environment.NewLine;
                query += "\t" + "ns0:state ?state;" + Environment.NewLine;
                query += "\t" + "ns0:Max ?max1;" + Environment.NewLine;
                query += "\t" + "ns0:Min ?min1]." + Environment.NewLine;
                query += "\t" + "FILTER (?min1 < " + val + " && ?max1 > " + val + ").}" + Environment.NewLine;

                query += "UNION" + Environment.NewLine + "{" + Environment.NewLine;

                query += "\t" + "[ns0:Par ns0:Interval;" + Environment.NewLine;
                query += "\t" + "ns0:state ?state;" + Environment.NewLine;
                query += "\t" + "ns0:Max ?max1;" + Environment.NewLine;
                query += "\t" + "ns0:Min ?min1]." + Environment.NewLine;
                query += "\t" + "FILTER (?min1 < " + val + " && ?max1 > " + val + ")." + Environment.NewLine;

                query +=
"}}";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return query;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (comboBox1.Text == "" || comboBox2.Text == "")
                {
                    textBox1.Text = "Please Select Port Settings"; //text box for recieve data
                }
                else
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                    serialPort1.Open();
                    progressBar1.Value = 100;

                   // button3.Enabled = true;   // 
                    button1.Enabled = true;  // button for open port
                }
                
            }
            catch (Exception)
            {
                
            }
            
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            this.Invoke(new EventHandler(tampil));
            this.Invoke(new EventHandler(tampil_1));
            this.Invoke(new EventHandler(tampil_2));
            this.Invoke(new EventHandler(tampil_3));
            this.Invoke(new EventHandler(tampil_4));
            this.Invoke(new EventHandler(tampil_5));


        }

        private void tampil_5(object sender, EventArgs e)
        {

            string sens1 = "Sensor1";
            string butt = "Button";
            string ReadPort1 = serialPort1.ReadLine();
            if (ReadPort1.Contains(butt))
            {
                string ReadPort11 = serialPort1.ReadLine();
                if (ReadPort11.Contains(sens1))
                {
                    string[] Sensor1 = ReadPort11.Split(' ');
                    string val = Sensor1[2];

                    textBox1.Text = "Nodes After Replace:" + Environment.NewLine;
                    var parser = new Notation3Parser();
                    var graph = new Graph();
                    parser.Load(graph, @"n3\\converter.n3");




                    Triple R = new Triple(
           graph.CreateUriNode(new Uri("http://www.semanticweb.org/laith/ontologies/2017/10/untitled-ontology-11#" + "Interval_1")),
           graph.CreateUriNode(new Uri("http://www.semanticweb.org/laith/ontologies/2017/10/untitled-ontology-11#" + "Max")),
           graph.CreateLiteralNode(Convert.ToString("20"), new Uri("http://www.w3.org/2001/XMLSchema#integer")));
                    graph.Retract(R);

                    Triple A = new Triple(
                         graph.CreateUriNode(new Uri("http://www.semanticweb.org/laith/ontologies/2017/10/untitled-ontology-11#" + "Interval_1")),
                         graph.CreateUriNode(new Uri("http://www.semanticweb.org/laith/ontologies/2017/10/untitled-ontology-11#" + "Max")),
                         graph.CreateLiteralNode((Convert.ToString(val)), new Uri("http://www.w3.org/2001/XMLSchema#integer")));
                    graph.Assert(A);

                    foreach (Triple triple in graph.Triples)
                    {
                        textBox1.Text += (GetNodeString(triple.Subject) + "    " + GetNodeString(triple.Predicate) + "    " + GetNodeString(triple.Object));
                        textBox1.Text += Environment.NewLine;
                    }
                }
            }

        }

        private void tampil_4(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                string sens5 = "Sensor5";
                string ReadPort5 = serialPort1.ReadLine();
                if (ReadPort5.Contains(sens5))
                {
                    string[] Sensor5 = ReadPort5.Split(' ');
                    string val = Sensor5[2];
                    string AlarmVar = "[ns0:Par_5 ns0:Interval_5;";
                    string WarningVar = "[ns0:Par5 ns0:interval_5;";
                    string query = GenSparqlQuery(AlarmVar, WarningVar, val);
                    SparqlResultSet resultSet = null;
                    resultSet = sparqlSend(query);
                    // textBox1.Text  = serialPort1.ReadLine();

                    for (int i = 0; i < resultSet.Count; i++)
                    {
                        SparqlResult result = resultSet[i];
                        
                        string q = Convert.ToString(result["state"]);

                        textBox6.Text = q;
                        if (q.Contains("Alarm"))
                        {
                            serialPort1.WriteLine("1");
                        }
                        else if (q.Contains("Warning"))
                        {
                            serialPort1.WriteLine("2");
                        }
                        else if (q.Contains("Normal"))
                        {
                            serialPort1.WriteLine("3");
                        }

                    }

                }

            }
        }

        private void tampil_3(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                string sens4 = "Sensor4";
                string ReadPort4 = serialPort1.ReadLine();
                if (ReadPort4.Contains(sens4))
                {
                    string[] Sensor4 = ReadPort4.Split(' ');
                    string val = Sensor4[2];
                    string AlarmVar = "[ns0:Par_4 ns0:Interval_4;";
                    string WarningVar = "[ns0:Par4 ns0:interval_4;";
                    string query = GenSparqlQuery(AlarmVar, WarningVar, val);
                    SparqlResultSet resultSet = null;
                    resultSet = sparqlSend(query);
                    // textBox1.Text  = serialPort1.ReadLine();

                    for (int i = 0; i < resultSet.Count; i++)
                    {
                        SparqlResult result = resultSet[i];
                         
                       string q = Convert.ToString(result["state"]);

                        textBox5.Text = q;
                        if (q.Contains("Alarm"))
                        {
                            serialPort1.WriteLine("1");
                        }
                        else if (q.Contains("Warning"))
                        {
                            serialPort1.WriteLine("2");
                        }
                        else if (q.Contains("Normal"))
                        {
                            serialPort1.WriteLine("3");
                        }

                    }

                }

            }
        }

        private void tampil_2(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                string sens3 = "Sensor3";
                string ReadPort3 = serialPort1.ReadLine();
                if (ReadPort3.Contains(sens3))
                {
                    string[] Sensor3 = ReadPort3.Split(' ');
                    string val = Sensor3[2];
                    string AlarmVar = "[ns0:Par_3 ns0:Interval_3;";
                    string WarningVar = "[ns0:Par3 ns0:interval_3;";
                    string query = GenSparqlQuery(AlarmVar, WarningVar, val);
                    SparqlResultSet resultSet = null;
                    resultSet = sparqlSend(query);
                    // textBox1.Text  = serialPort1.ReadLine();

                    for (int i = 0; i < resultSet.Count; i++)
                    {
                        SparqlResult result = resultSet[i];
                        
                       string q = Convert.ToString(result["state"]);

                        textBox4.Text = q;
                        if (q.Contains("Alarm"))
                        {
                            serialPort1.WriteLine("1");
                        }
                        else if (q.Contains("Warning"))
                        {
                            serialPort1.WriteLine("2");
                        }
                        else if (q.Contains("Normal"))
                        {
                            serialPort1.WriteLine("3");
                        }

                    }

                }

            }
        }

        private void tampil_1(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                
                string sens2 = "Sensor2";
                string ReadPort2 = serialPort1.ReadLine();
                if (ReadPort2.Contains(sens2))
                {
                    
                    string[] Sensor2 = ReadPort2.Split(' ');
                    string val = Sensor2[2];
                    string AlarmVar = "[ns0:Par_2 ns0:Interval_2;";
                    string WarningVar = "[ns0:Par2 ns0:interval_2;";
                    string query = GenSparqlQuery(AlarmVar, WarningVar, val);
                    SparqlResultSet resultSet = null;
                    resultSet = sparqlSend(query);
                    // textBox1.Text  = serialPort1.ReadLine();

                    for (int i = 0; i < resultSet.Count; i++)
                    {
                        SparqlResult result = resultSet[i];
                        string q = Convert.ToString(result["state"]);

                        textBox3.Text = q;
                        if (q.Contains("Alarm"))
                        {
                            serialPort1.WriteLine("1");
                        }
                        else if (q.Contains("Warning"))
                        {
                            serialPort1.WriteLine("2");
                        }
                        else if (q.Contains("Normal"))
                        {
                            serialPort1.WriteLine("3");
                        }
                        
                    }

                }

            }
        }

        private void tampil(object sender, EventArgs e)
        {
            
            if (checkBox1.Checked)
            {
                string sens1 = "Sensor1";
                string ReadPort1 = serialPort1.ReadLine();
                if (ReadPort1.Contains(sens1))
                {
                    string[] Sensor1 = ReadPort1.Split(' ');
                    string val = Sensor1[2];
                    string AlarmVar = "[ns0:Par_1 ns0:Interval_1;";
                    string WarningVar = "[ns0:Par1 ns0:interval_1;";
                    string query = GenSparqlQuery(AlarmVar, WarningVar, val);
                    SparqlResultSet resultSet = null;
                    resultSet = sparqlSend(query);
                    // textBox1.Text  = serialPort1.ReadLine();
                   
                    for (int i = 0; i < resultSet.Count; i++)
                        {
                            SparqlResult result = resultSet[i];
                            
                             string q = Convert.ToString(result["state"]);
                        
                        textBox2.Text = q;
                        
                        if (q.Contains("Alarm"))
                        {
                            serialPort1.WriteLine("1");
                        }
                        else if (q.Contains("Warning"))
                        {
                            serialPort1.WriteLine("2");
                        }
                        else if (q.Contains("Normal"))
                        {
                            serialPort1.WriteLine("3");
                        }
                    }
                    
                }

            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            progressBar1.Value = 0;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Nodes:" + Environment.NewLine;
            var parser = new Notation3Parser();
            var graph = new Graph();
          parser.Load(graph, @"n3\\converter.n3");


            foreach (Triple triple in graph.Triples)
            {
                textBox1.Text += (GetNodeString(triple.Subject) + "    " + GetNodeString(triple.Predicate) + "    " + GetNodeString(triple.Object));
                textBox1.Text += Environment.NewLine;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine("1,2");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine("3,4");
        }

        private string GetNodeString(INode node)
        {
            string s = node.ToString();
            switch (node.NodeType)
            {
                case NodeType.Uri:
                    int lio = s.LastIndexOf('#');
                    if (lio == -1)
                        return s;
                    else
                        return s.Substring(lio + 1);
                case NodeType.Literal:
                    return string.Format("\"{0}\"", s);
                default:
                    return s;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {



        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
