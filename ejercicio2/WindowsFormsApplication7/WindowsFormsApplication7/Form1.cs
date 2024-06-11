using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.Odbc;

namespace WindowsFormsApplication7
{

    public partial class Form1 : Form
    {
        int cR, cG, cB;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "archivos jpg|*.jpg";
            openFileDialog1.ShowDialog();
            Bitmap bmp = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = bmp;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            /*Bitmap bmp = new Bitmap(pictureBox1.Image);
            Color c = new Color();
            c = bmp.GetPixel(e.X, e.Y);
            textBox1.Text = c.R.ToString();
            textBox2.Text = c.G.ToString();
            textBox3.Text = c.B.ToString();
             */
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Color c = new Color();
            int sR, sG, sB;
            sR = 0;
            sG = 0;
            sB = 0;
            for (int i = e.X; i < e.X + 10;i++)
                for (int j = e.Y; j < e.Y + 10; j++)
                { 
                    c = bmp.GetPixel(i, j);
                    sR = sR + c.R;
                    sG = sG + c.G;
                    sB = sB + c.B;
                }
            sR = sR/100;
            sG = sG/100;
            sB = sB/100;
            cR = sR;
            cG = sG;
            cB = sB;
            textBox1.Text = sR.ToString();
            textBox2.Text = sG.ToString();
            textBox3.Text = sB.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            Color c = new Color();
            for (int i=0;i<bmp.Width;i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);
                    if (((74 <= c.R) && (c.R <= 104)) && ((84 <= c.G) && (c.G <= 114)) && ((74 <= c.B) && (c.B <= 104)))
                        bmp2.SetPixel(i, j, Color.Black);
                    else
                        bmp2.SetPixel(i, j, Color.FromArgb(c.R, c.G, c.B));
                }
            pictureBox1.Image = bmp2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            Color c = new Color();
            int sR, sG, sB;
            for (int i = 0; i < bmp.Width-10; i=i+10)
                for (int j = 0; j < bmp.Height-10; j=j+10)
                {
                    sR = 0; sG = 0; sB = 0;
                    for (int ip = i; ip < i + 10; ip++)
                        for (int jp = j; jp < j + 10; jp++)
                        {
                            c = bmp.GetPixel(ip, jp);
                            sR = sR + c.R;
                            sG = sG + c.G;
                            sB = sB + c.B;
                        }
                    sR = sR / 100;
                    sG = sG / 100;
                    sB = sB / 100;

                    if (((cR - 20 <= sR) && (sR <= cR + 20)) && ((cG - 20 <= sG) && (sG <= cG + 20)) && ((cB - 20 <= sB) && (sB <= cB + 20)))
                        {
                            for (int ip = i; ip < i + 10; ip++)
                                for (int jp = j; jp < j + 10; jp++)
                                {
                                    bmp2.SetPixel(ip, jp, Color.Black);
                                }
                        }
                    else
                    {
                        for (int ip = i; ip < i + 10; ip++)
                            for (int jp = j; jp < j + 10; jp++)
                            {
                                c = bmp.GetPixel(ip, jp);
                                bmp2.SetPixel(ip, jp, Color.FromArgb(c.R, c.G, c.B));
                            }
                    }
                        
                }
            pictureBox1.Image = bmp2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OdbcConnection con = new OdbcConnection();
            OdbcCommand cmd = new OdbcCommand();

            con.ConnectionString = "DSN=prueba";

            cmd.CommandText = "insert into texturas(descripcion,cR,cG,cB,colorpintar) ";
            cmd.CommandText = cmd.CommandText + " values('"+textBox4.Text+"',"+textBox1.Text+","+textBox2.Text+","+textBox3.Text+",'"+textBox5.Text+"')";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            mostrar();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Read inputs from the text boxes
            string textureDescription = textBox6.Text;
           

            if (string.IsNullOrEmpty(textureDescription))
            {
                MessageBox.Show("Please enter both texture description and color.");
                return;
            }

            

            try
            {
                using (OdbcConnection con = new OdbcConnection("DSN=prueba"))
                {
                    string query = $"SELECT cR, cG, cB, colorpintar FROM texturas WHERE descripcion = '{textureDescription}'";
                    using (OdbcCommand cmd = new OdbcCommand(query, con))
                    {
                        // Add parameters to the command
                       

                        con.Open();
                        String aux = "";
                        Console.WriteLine("Executing query: " + query);
                        OdbcDataReader reader = cmd.ExecuteReader();
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("No texture found with the given description and color.");
                            return;
                        }
                        while (reader.Read()) {
                            int cR = reader.GetInt32(reader.GetOrdinal("cR"));
                            int cG = reader.GetInt32(reader.GetOrdinal("cG"));
                            int cB = reader.GetInt32(reader.GetOrdinal("cB"));
                            Console.WriteLine("Executin: " + cR.ToString() + " " + cG.ToString() + cB.ToString());
                            string color = reader.GetString(reader.GetOrdinal("colorpintar"));
                            int a=PaintBasedOnColor(cR, cG, cB, color);
                            if(a>0)
                                aux = aux + " " + color;

                        }
                        MessageBox.Show(aux);
                    }
                }

                
            }
            catch (OdbcException odbcEx)
            {
                // ODBC specific error details
                MessageBox.Show("ODBC error occurred: " + odbcEx.Message + "\nStack Trace:\n" + odbcEx.StackTrace);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            
        }
        private int PaintBasedOnColor(int cR, int cG, int cB, string color)
        {
            int res=0;
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            Color c = new Color();
            Color colorpintar = ColorTranslator.FromHtml(color);
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);
                    if (((cR - 20 <= c.R) && (c.R <= cR + 20)) && ((cG - 20 <= c.G) && (c.G <= cG + 20)) && ((cB - 20 <= c.B) && (c.B <= cB + 20)))
                    {
                        bmp2.SetPixel(i, j, colorpintar);
                        res =res+ 1;
                    }
                    else
                        bmp2.SetPixel(i, j, Color.FromArgb(c.R, c.G, c.B));
                }
            }
            pictureBox1.Image = bmp2;
            return res;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mostrar();
        }

        private void mostrar()
        {
            //using System.Data;
            //using System.Data.Odbc;
            OdbcConnection con = new OdbcConnection();
            OdbcDataAdapter ada = new OdbcDataAdapter();
            con.ConnectionString = "DSN=prueba";
            ada.SelectCommand = new OdbcCommand();
            ada.SelectCommand.Connection = con;
            ada.SelectCommand.CommandText = "select * from texturas";
            DataSet ds = new DataSet();
            ada.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
