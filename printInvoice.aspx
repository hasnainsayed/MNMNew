<%@ Page Language="C#" AutoEventWireup="true" CodeFile="printInvoice.aspx.cs" Inherits="printInvoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body id="getthis">
    <div runat="server" id="err"></div>
         <p><b>PACKAGING LIST WITH ESTIMATE MEMO</b></p>
                            <table width="100%" style="border-collapse: collapse ;border:1px solid;text-align:left" border="1px;" cellpadding="30px" cellspacing="3">
		<tr style="border-bottom:white;">
			<td width="30%" style="padding : 1px 10px;">Customer</td>
            <td width="30%" style="padding : 1px 10px;">Contact No</td>
			<td width="40%" style="padding : 1px 10px;">On Approval Code:</td>
			<td width="30%" style="padding : 1px 10px;">Date</td>
		</tr>
		<tr>
			<th style="border-bottom:white;padding : 1px 10px;"><b><asp:Label ID="custname" runat="server" Text=""></asp:Label></b></th>
            <th><asp:Label ID="phoneNo" runat="server" Text=""></asp:Label></th>
			<th style="padding : 1px 10px;"><asp:Label ID="invoicenum" runat="server" Text="Label"></asp:Label>
              <asp:Label ID="stateID" runat="server" visible="false"></asp:Label>
                <br />
              <asp:Label ID="remarks" runat="server" visible="true"></asp:Label>           
			</th>
			<th style="padding : 1px 10px;"><asp:Label ID="invoicedate" runat="server" Text=""></asp:Label></th>
            
		</tr>
		
		
		<tr>
			<td colspan="4" style="padding : 1px 10px;">
				<table width="100%" style="border-collapse: collapse ;border:1px solid;text-align:left;" border="1px;" cellpadding="3" cellspacing="0">
					<tr>
						<th style="padding : 1px 10px;">Check No</th>
						<th style="padding : 1px 10px;">Descriptions of Goods</th>
                        <th style="padding : 1px 10px;">PC/Pkt</th>
                        <th style="padding : 1px 10px;">Rate/Pc</th>
                        <th style="padding : 1px 10px;">Packet Rate</th>
						<th style="padding : 1px 10px;">No. of Packets</th>
                        <th style="padding : 1px 10px;">Amount (INR)</th>

                        <th style="padding : 1px 10px;" runat="server"  visible="false">Pieces</th>
						
						<th style="padding : 1px 10px;" runat="server"  visible="false">Taxable Value(INR)</th>
						<th style="padding : 1px 10px;" runat="server" id="igstHeader" visible="false">IGST (INR)</th>
						<th style="padding : 1px 10px;" runat="server" id="cgstHeader" visible="false">CGST (INR)</th>
						<th style="padding : 1px 10px;" runat="server" id="sgstHeader" visible="false">SGST (INR)</th>
						
					</tr>
                    <asp:Repeater ID="rpt_Invoice" runat="server" ><%--OnItemDataBound="rpt_Invoice_ItemDataBound"--%>
                                    <ItemTemplate>
					    <tr>
						    <td style="padding : 1px 10px;"><%# Eval("checkNo").ToString() %> </td>
						    <td style="padding : 1px 10px;"><%# Eval("Title").ToString() %></td>					    
						    <td style="padding : 1px 10px;"><%# Eval("piecePerPacket").ToString() %></td>
                            <td style="padding : 1px 10px;"><%# System.Math.Round(Convert.ToDecimal(Eval("sellingprice"))/Convert.ToInt32(Eval("piecePerPacket")),2) %></td>                
                            <td style="padding : 1px 10px;"><%# Eval("sellingprice").ToString() %></td>
                            <td style="padding : 1px 10px;"><%# Eval("Qnty").ToString() %></td>
                            <td style="padding : 1px 10px;"><%# Eval("totalAmount").ToString() %></td>


                            <td style="padding : 1px 10px;" runat="server" visible="false"><%# Eval("Pieces").ToString() %></td>
                            <td style="padding : 1px 10px;" runat="server" visible="false"><%# Eval("taxableamount").ToString() %></td>                            											
						    <td style="padding : 1px 10px;" visible="false" runat="server" id="igstMiddle"><%# Eval("igstamnt").ToString() %> (<%# Eval("gstpercent").ToString() %>) %</td>
						    <td style="padding : 1px 10px;" visible="false" runat="server" id="cgstMiddle"><%# Eval("cgstamnt").ToString() %> (<%# (Convert.ToDecimal(Eval("gstpercent"))/2).ToString() %>) %</td>
						    <td style="padding : 1px 10px;" visible="false" runat="server" id="sgstMiddle"><%# Eval("sgstamnt").ToString() %> (<%# (Convert.ToDecimal(Eval("gstpercent"))/2).ToString() %>)  %</td>
						    	
					    </tr>
                       </ItemTemplate>
                     </asp:Repeater>
					<tr>
						<td style="padding : 1px 10px;"></td>
						
						<td style="padding : 1px 10px;"></td>
						<td style="padding : 1px 10px;" runat="server" visible="false"></td>
						<td style="padding : 1px 10px;" runat="server" visible="false"></td>
                        <td style="padding : 1px 10px;"></td>
                        <td style="padding : 1px 10px;"></td>
						<td style="padding : 1px 10px;" runat="server" visible="false"><asp:Label runat="server" ID="totTaxableAmnt"></asp:Label></td>
						<td style="padding : 1px 10px;" runat="server" id="igstFooter" visible="false"><asp:Label runat="server" ID="totIgst"></asp:Label></td>
						<td style="padding : 1px 10px;" runat="server" id="cgstFooter" visible="false"><asp:Label runat="server" ID="totCgast"></asp:Label></td>
						<td style="padding : 1px 10px;" runat="server" id="sgstFooter" visible="false"><asp:Label runat="server" ID="totSgst"></asp:Label></td>
                        <td></td>
                        <td style="padding : 1px 10px;"><b>TOTAL (Taxes As Applicable):</b></td>
						<td style="padding : 1px 10px;"><b><asp:Label ID="totalamnt" runat="server" Text=""></asp:Label></b></td>
					</tr>
                    <tr>
                        <th colspan="5" style="text-align:right;"><asp:Label runat="server" ID="totQnty"></asp:Label> pkts : <asp:Label runat="server" ID="totalPieces"></asp:Label> Pcs</th>
                        <th colspan="6"></th>
                    </tr>
				</table>
			</td>
		</tr>
		<tr style="vertical-align:top;">
			<td colspan="3" style="padding : 1px 10px;">
				Amount Chargeable (in words)</br>
				<b>INR <asp:Label ID="amntwords" runat="server" Text=""></asp:Label> Only</br>
				</b>
			</td>
			<td style="text-align:right;padding : 1px 10px;" ><b>E. & O.E</b></td>
		</tr>
		<tr style="vertical-align:top;">
			<td>Package Details :</td>
            <td>Checked :</td>
            <td>Tallied :</td>
            <td>Verified & Approved :</td>
		</tr>
                                <tr>
                                    <td></td><td></td><td></td><td></td>
                                </tr>
	</table>
	<p style="text-align:center"><b>This is a Computer Generated Invoice</b></p>
</body>
</html>
