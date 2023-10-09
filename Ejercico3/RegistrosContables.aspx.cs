using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejercico3
{
    public partial class RegistrosContables : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                actualizarLabel();
            }

        }
        protected void actualizarLabel()
        {
            Label1.Text = "";
            DataView dv = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);

        }


        protected void completarTabla()
        {
            try
            {
                DataView dv = (DataView)SqlDataSource3.Select(DataSourceSelectArguments.Empty);


                if (dv != null && dv.Count > 0)
                {
                    // Rellenar cabecera
                    TableRow headerRow = new TableRow();

                    TableCell headerCell1 = new TableCell();
                    headerCell1.Text = "descripcion";
                    headerRow.Cells.Add(headerCell1);

                    TableCell headerCell2 = new TableCell();
                    headerCell2.Text = "id";
                    headerRow.Cells.Add(headerCell2);

                    TableCell headerCell3 = new TableCell();
                    headerCell3.Text = "monto";
                    headerRow.Cells.Add(headerCell3);

                    Table1.Rows.Add(headerRow);

                    // Rellenar las filas
                    foreach (DataRowView rowView in dv)
                    {
                        DataRow row = rowView.Row;
                        TableRow tableRow = new TableRow();

                        TableCell cell1 = new TableCell();
                        cell1.Text = row["descripcion"].ToString();
                        tableRow.Cells.Add(cell1);

                        TableCell cell2 = new TableCell();
                        cell2.Text = row["id"].ToString();
                        tableRow.Cells.Add(cell2);

                        TableCell cell3 = new TableCell();
                        cell3.Text = row["monto"].ToString();
                        tableRow.Cells.Add(cell3);

                        Table1.Rows.Add(tableRow);
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", $"alert('Error');", true);
            }
        }


        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {


        }

        protected void Button1_Click(object sender, EventArgs e) //Agregar
        {
            SqlDataSource2.InsertParameters["monto"].DefaultValue = TextBox1.Text;
            SqlDataSource2.InsertParameters["idCuenta"].DefaultValue = DropDownList1.SelectedValue;
            //SqlDataSource2.InsertParameters["tipo"].DefaultValue = TextBox4.Text;
            int result = SqlDataSource2.Insert();
            if (result > 0)
            {
                Label1.Text = "Agregado " + result.ToString() + " registro.";
                TextBox1.Text = string.Empty;
                completarTabla();
            }
            else
            {
                Label1.Text = "No se agregaron registros.";
            }

        }

        protected void Button2_Click(object sender, EventArgs e) // Borrado
        {
            int result = SqlDataSource2.Delete();
            if (result > 0)
            {
                Label2.Text = "Se ha eliminado" + result.ToString() + " registros";
                actualizarLabel();
                TextBox1.Text = "";
                completarTabla();
            }
            else
            {
                Label2.Text = "NO se Eliminaro los registros";
            }

        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if(TextBox2.Text != "" && TextBox3.Text != "" && RadioButtonList1.SelectedIndex ==0)
            {
                calcularDebe();
            }
            else
            {
   
            if (TextBox2.Text != "" && TextBox3.Text != "" && RadioButtonList1.SelectedIndex == 1)
                    {
                        calcularHaber();
                    }
            else
                    try
                    {
                        // Validar que el campo de nombre no esté vacío
                        if (string.IsNullOrWhiteSpace(TextBox2.Text))
                        {
                            throw new Exception("El campo de nombre no puede estar vacío.");
                        }

                        // Validar que el campo de edad no esté vacío y que sea un número válido
                        if (string.IsNullOrWhiteSpace(TextBox2.Text) || !int.TryParse(TextBox3.Text, out int t3))
                        {
                            throw new Exception("los campos debe ser un números válidos.");
                        }

                        }
                    catch (Exception ex)
                    {
                        // Manejar la excepción mostrando un mensaje de error al usuario
                        Label3.Text = "Error: " + ex.Message;
                    }








            }
        }


       private void calcularDebe()
        {
            int t2, t3;
            float resul;

            t2 = int.Parse(TextBox2.Text);
            t3 = int.Parse(TextBox3.Text);

            resul = t2+t3;
            TextBox4.Text = resul.ToString();
        }

        private void calcularHaber()
        {
            int t2, t3;
            float resul;

            t2 = int.Parse(TextBox2.Text);
            t3 = int.Parse(TextBox3.Text);

            resul = t2 - t3;
            TextBox4.Text = resul.ToString();
        }
    }
}