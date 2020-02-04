using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class hexConversion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Store integer 182
        int decValue = 99999;
        // Convert integer 182 as a hex in a string variable
        string hexValue = decValue.ToString("X4");
        // Convert the hex string back to the number
        int decAgain = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);

        hex.Text = hexValue;
        ints.Text = decAgain.ToString();
    }
}