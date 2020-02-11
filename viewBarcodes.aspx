<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="viewBarcodes.aspx.cs" Inherits="viewBarcodes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
    function CallPrint(strid) {
        var headstr = "<html><head><title></title></head><body>";
  var footstr = "</body>";
  var newstr = document.all.item(strid).innerHTML;
  var oldstr = document.body.innerHTML;
  document.body.innerHTML = headstr+newstr+footstr;
  window.print();
  document.body.innerHTML = oldstr;
  return false;
    }
</script>

    <div class="content-wrapper"> <section class="content-header">
        <h1>Invoice List
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active"><a href="invoice.aspx">Invoice List</a></li>
            <li class="active">View Invoice </li>
        </ol>
    </section>
         
    <section class="content">
              <!-- Default box -->
        <div class="box">
            <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">View Invoice </h3>
                        <asp:Button ID="Button1" runat="server" Text="Print" onclientclick="javascript:CallPrint('print');" class="btn btn-sm btn-danger pull-right"/>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row" id="print">
                           


                            <table width="100%" style="border-collapse: collapse ;border:1px solid;text-align:left" border="1px;" cellpadding="30px" cellspacing="3">
		<tr style="border-bottom:white;">
			<td width="30%" style="padding : 1px 10px;">Sender</td>
			<td width="40%" style="padding : 1px 10px;">On Approval Code:</td>
			<td width="30%" style="padding : 1px 10px;"> Date</td>
		</tr>
		<tr>
			<th style="border-bottom:white;padding : 1px 10px;"></th>
			<th style="padding : 1px 10px;"><asp:Label ID="invoicenum" runat="server" Text="Label"></asp:Label>
              <asp:Label ID="stateID" runat="server" visible="false"></asp:Label>
			</th>
			<th style="padding : 1px 10px;"><asp:Label ID="invoicedate" runat="server" Text=""></asp:Label></th>
		</tr>
		<tr>
			<td style="border-top:white;padding : 1px 10px;">
			
			</td>
			<td style="text-align:center;vertical-align:top;padding : 1px 10px;">
				Order No: <asp:Label ID="salesIDgivenbyVloc" runat="server" Text=""></asp:Label></br></br>
				Order Date: <asp:Label ID="orderDate" runat="server" Text=""></asp:Label></br>
                <asp:PlaceHolder ID="plBarCode" runat="server" />
			</td>
			<td style="vertical-align:top;padding : 1px 10px;">
				<p style="margin-block-start: 0.2px!important;margin-block-end: 3px!important;">Portal</p>
				<p style="margin-block-start: 0.2px!important;margin-block-end: 5px!important;"><b><asp:Label ID="portal" runat="server" Text=""></asp:Label></b></p>
				<p style="margin-block-start: 0.2px!important;margin-block-end: 3px!important;">Payment Mode</p>
				<p style="margin-block-start: 0.2px!important;margin-block-end: 0.2px!important;"><b><asp:Label ID="paymentMode" runat="server" Text=""></asp:Label></b></p>
			</td>
		</tr>
		<tr style="vertical-align:top;border-bottom:white;">
			<td style="padding : 1px 10px;">Bill To:</br>
				<b><asp:Label ID="custname" runat="server" Text=""></asp:Label></b></br>
				<asp:Label ID="address1" runat="server" Text=""></asp:Label></br>
				<asp:Label ID="address2" runat="server" Text=""></asp:Label></br>
				<asp:Label ID="cityname" runat="server" Text=""></asp:Label></br>
				<asp:Label ID="statename" runat="server" Text=""></asp:Label></br>
				T: <asp:Label ID="phoneNo" runat="server" Text=""></asp:Label>
			</td>
			<td style="padding : 1px 10px;">Ship To:</br>
				<b><asp:Label ID="custname1" runat="server" Text=""></asp:Label></b></br>
				<asp:Label ID="address11" runat="server" Text=""></asp:Label></br>
				<asp:Label ID="address21" runat="server" Text=""></asp:Label></br>
				<asp:Label ID="cityname1" runat="server" Text=""></asp:Label></br>
				<asp:Label ID="statename1" runat="server" Text=""></asp:Label></br>
				T: <asp:Label ID="phoneNo1" runat="server" Text=""></asp:Label>
			</td>
			<td style="padding : 1px 10px;">
				<p style="margin-block-start: 0.2px!important;margin-block-end: 3px!important;">Dispatch Through</p>
				<p style="margin-block-start: 0.2px!important;margin-block-end: 5px!important;"><b><asp:Label ID="courierName" runat="server" Text=""></asp:Label></b></p>
				<p style="margin-block-start: 0.2px!important;margin-block-end: 3px!important;">AWB No</p>
				<p style="margin-block-start: 0.2px!important;margin-block-end: 0.2px!important;"><b><asp:Label ID="salesABWNo" runat="server" Text=""></asp:Label></b></p>
                <p><asp:PlaceHolder ID="plBarCode1" runat="server" /></p>
			</td>
		</tr>
		<tr>
			<td colspan="3" style="padding : 1px 10px;">
				<table width="100%" style="border-collapse: collapse ;border:1px solid;text-align:left;" border="1px;" cellpadding="3" cellspacing="0">
					<tr>
						<th style="padding : 1px 10px;">SI No.</th>
						<th style="padding : 1px 10px;">Descriptions of Goods</th>
						<th style="padding : 1px 10px;">Part No</th>
						<th style="padding : 1px 10px;">Qty</>
						<th style="padding : 1px 10px;">Rate</th>
						<th style="padding : 1px 10px;">Taxable Value(INR)</th>
						<th style="padding : 1px 10px;" runat="server" id="igstHeader" visible="false">IGST (INR)</th>
						<th style="padding : 1px 10px;" runat="server" id="cgstHeader" visible="false">CGST (INR)</th>
						<th style="padding : 1px 10px;" runat="server" id="sgstHeader" visible="false">SGST (INR)</th>
						<th style="padding : 1px 10px;">Amount (INR)</th>
					</tr>
                    <asp:Repeater ID="rpt_Invoice" runat="server" OnItemDataBound="rpt_Invoice_ItemDataBound">
                                    <ItemTemplate>
					    <tr>
						    <td style="padding : 1px 10px;"><%# Container.ItemIndex + 1 %></td>
						    <td style="padding : 1px 10px;"><%# Eval("Title").ToString() %></td>
						    <td style="padding : 1px 10px;">Barcode : <%# Eval("BarcodeNo").ToString() %> </br> Article No : <%# Eval("Control3").ToString() %></td>
						    <td style="padding : 1px 10px;">1</td>
                            <td style="padding : 1px 10px;"><%# Eval("taxableamount").ToString() %></td>
						    <td style="padding : 1px 10px;"><%# Eval("taxableamount").ToString() %></td>						
						    <td style="padding : 1px 10px;" visible="false" runat="server" id="igstMiddle"><%# Eval("igstamnt").ToString() %> (<%# Eval("gstpercent").ToString() %>) %</td>
						    <td style="padding : 1px 10px;" visible="false" runat="server" id="cgstMiddle"><%# Eval("cgstamnt").ToString() %> (<%# (Convert.ToDecimal(Eval("gstpercent"))/2).ToString() %>) %</td>
						    <td style="padding : 1px 10px;" visible="false" runat="server" id="sgstMiddle"><%# Eval("sgstamnt").ToString() %> (<%# (Convert.ToDecimal(Eval("gstpercent"))/2).ToString() %>)  %</td>
						    <td style="padding : 1px 10px;"><%# Eval("sellingprice").ToString() %></td>
					    </tr>
                       </ItemTemplate>
                     </asp:Repeater>
                    <tr>
						<td style="padding : 1px 10px;"></td>
						<td style="padding : 1px 10px;">Shipping Charge</td>
						<td style="padding : 1px 10px;"></td>
						<td style="padding : 1px 10px;"></td>
						<td style="padding : 1px 10px;"></td>
						<td style="padding : 1px 10px;"></td>
						<td style="padding : 1px 10px;" runat="server" id="Td1" visible="false"></td>
						<td style="padding : 1px 10px;" runat="server" id="Td2" visible="false"></td>
						<td style="padding : 1px 10px;" runat="server" id="Td3" visible="false"></td>
						<td style="padding : 1px 10px;"><asp:Label ID="shippingCharge" runat="server" Text=""></asp:Label></td>
					</tr>
					<tr>
						<td style="padding : 1px 10px;"></td>
						<td style="padding : 1px 10px;">Total:</td>
						<td style="padding : 1px 10px;"></td>
						<td style="padding : 1px 10px;"><asp:Label runat="server" ID="totQnty"></asp:Label></td>
						<td style="padding : 1px 10px;"></td>
						<td style="padding : 1px 10px;"><asp:Label runat="server" ID="totTaxableAmnt"></asp:Label></td>
						<td style="padding : 1px 10px;" runat="server" id="igstFooter" visible="false"><asp:Label runat="server" ID="totIgst"></asp:Label></td>
						<td style="padding : 1px 10px;" runat="server" id="cgstFooter" visible="false"><asp:Label runat="server" ID="totCgast"></asp:Label></td>
						<td style="padding : 1px 10px;" runat="server" id="sgstFooter" visible="false"><asp:Label runat="server" ID="totSgst"></asp:Label></td>
						<td style="padding : 1px 10px;"><asp:Label ID="totalamnt" runat="server" Text=""></asp:Label></td>
					</tr>
				</table>
			</td>
		</tr>
		<tr style="vertical-align:top;">
			<td colspan="2" style="padding : 1px 10px;">
				Amount Chargeable (in words)</br>
				<b>INR <asp:Label ID="amntwords" runat="server" Text=""></asp:Label> Only</br>
				Tax is payable on reverse charge basis: No</b>
			</td>
			<td style="text-align:right;padding : 1px 10px;"><b>E. & O.E</b></td>
		</tr>
		<tr style="vertical-align:top;">
			<td colspan="2" style="padding : 1px 10px;">
				Declaration</br>
				
			</td>
			<td style="text-align:center;padding : 1px 10px;"><b> </br></br></br>Authorised Signatory</b></td>
		</tr>
	</table>
	<p style="text-align:center"><b>This is a Computer Generated Invoice</b></p>

                          
                         
                        </div>


                        </div></div></div></div>
        </section>

        </div>
</asp:Content>



