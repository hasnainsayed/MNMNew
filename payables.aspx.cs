using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class payables : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                BindData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mykey", "firedtSearch();", true);
            }
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void BindData()
    {
        try
        {
            // bind headers
            styleCls obj = new styleCls();
            DataTable dt = obj.getTable("paymentCheckPoint");
            checkColumn1.Text = checkColumn1Th.Text = "Under " +dt.Rows[0]["checkOne"].ToString();
            checkColumn2.Text = checkColumn2Th.Text = (Convert.ToInt32(dt.Rows[0]["checkOne"])+1).ToString()+"-"+dt.Rows[0]["checkTwo"].ToString();
            checkColumn3.Text = checkColumn3Th.Text = (Convert.ToInt32(dt.Rows[0]["checkTwo"]) + 1).ToString() + "-" + dt.Rows[0]["checkThree"].ToString();
            checkColumn4.Text = checkColumn4Th.Text = dt.Rows[0]["checkThree"].ToString()+"+";

            lotPaymentCls r = new lotPaymentCls();
            DataTable st = r.getAllPayables();
            rtp_List.DataSource = st;
            rtp_List.DataBind();

            // compute sum
            object sum1;
            sum1 = st.Compute("sum(checkOneAmount)", string.Empty);
            checkColumn1Sum.Text = sum1.ToString();

            object sum2;
            sum2 = st.Compute("sum(checkTwoAmount)", string.Empty);
            checkColumn2Sum.Text = sum2.ToString();

            object sum3;
            sum3 = st.Compute("sum(checkThreeAmount)", string.Empty);
            checkColumn3Sum.Text = sum3.ToString();

            object sum4;
            sum4 = st.Compute("sum(checkFourAmount)", string.Empty);
            checkColumn4Sum.Text = sum4.ToString();

            object sum5;
            sum5 = st.Compute("sum(totalAmount)", string.Empty);
            total.Text = sum5.ToString();
        }
        catch (Exception ex)
        {
            RecordExceptionCls rec = new RecordExceptionCls();
            rec.recordException(ex);
        }
    }

    protected void vendorPayable_Click(object sender, EventArgs e)
    {

    }
}