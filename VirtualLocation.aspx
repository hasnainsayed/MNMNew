<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VirtualLocation.aspx.cs" Inherits="VirtualLocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Location
            </h1>
            <ol class="breadcrumb">
                <li><a href="index.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Master</li>
                <li class="active">Location</li>

            </ol>
        </section>
        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
            <ContentTemplate>

                <!-- Form  -->
                <div class="box box-primary" id="ctfrmDet" runat="server" visible="false">
                    <div class="box-body">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Select Location Type</label>
                                <asp:RequiredFieldValidator ControlToValidate="ddlLocationType" ID="selectproj"
                                    ValidationGroup="grp" ErrorMessage="Please select project" ForeColor="Red"
                                    InitialValue="0" runat="server" Display="Dynamic" />

                                <asp:DropDownList ID="ddlLocationType" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control">
                                    <%--<asp:ListItem Text="---- Select ----" Value="0"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <%-- <div class="col-md-4">
                            <div class="form-group">
                                <label>Select Location Type2</label>
                                <asp:RequiredFieldValidator ControlToValidate="ddlLocationType2" ID="RequiredFieldValidator3"
                                    ValidationGroup="grp" ErrorMessage="Please select project" ForeColor="Red"
                                    InitialValue="0" runat="server" Display="Dynamic" />

                                <asp:DropDownList ID="ddlLocationType2" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>--%>

                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Location</label>
                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" ID="reqname" ErrorMessage="Please enter name" ControlToValidate="txtName" Display="Dynamic" />
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Contact</label>
                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" ID="RequiredFieldValidator1" ErrorMessage="Please enter name" ControlToValidate="txtContact" Display="Dynamic" />
                                <asp:TextBox ID="txtContact" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-8">
                            <div class="form-group">
                                <label>Address</label>
                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" ID="RequiredFieldValidator2" ErrorMessage="Please enter name" ControlToValidate="txtAddress" Display="Dynamic" />
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        </div>
                    </div>
       <div class="box box-primary" id="divpymtsetting" runat="server" visible="false">
                    <div class="box-body">
                        <asp:Label ID="lblhdID" runat="server" Text="" Visible="false"></asp:Label>
                         <div class="row">
                           <div class="col-xs-12">
               <%--<asp:Label ID="lblvlocid" runat="server" Text="" Visible="false"></asp:Label>--%>
            
                
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title" style="font-weight:bold">Payment File Setting:</h3>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       
                         <h3 class="box-title" runat="server" id="h3vloc" visible="false" style="font-weight:bold;align-content:center">V Location# &nbsp;<asp:Label ID="lblvlocname" runat="server" Font-Bold="true" ForeColor="red" Text="" ></asp:Label><i class="fa  fa-home"></i></h3>
                            
                          </div>
                    <div class="box-body table-responsive">
                         
                        <table id="example111" class="table table-bordered table-striped ">
                            <thead>
                               <tr>
                                   <th>DB Column</th>
                                   <th>Payment Column</th>
                               </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Sales ID
                                        <asp:Label ID="lblstockupId" runat="server" Text="stockupId" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtstockupId" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td>Type
                                        <asp:Label ID="lbltype" runat="server" Text="type" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txttype" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                               
                                 <tr>
                                    <td><div class="col-md-2">
                                        Selling Price &nbsp;&nbsp;&nbsp;&nbsp;</div>
                                        <div class="col-md-10">
                                        <asp:TextBox ID="txtMRP" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                       
                                        <asp:Label ID="lblMRP" runat="server" Text="MRP" Visible="false"></asp:Label> 
                                    </td>
                                    <td> 
                                        <div class="col-md-6">Selling Price is included of GST&nbsp;&nbsp;&nbsp;&nbsp; 
                                        <asp:CheckBox ID="chksalsprice" runat="server"  />
                                            </div>
                                        <%--<div class="col-md-4">
                                         GST Applicable(if First Checkbox is Not Selected)&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="checkgst" runat="server" /> 
                                            </div>--%>
                                         <div class="col-md-6">
                                         Is the payment is depend upon Status/Type &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chckstatus" runat="server" OnCheckedChanged="chckstatus_CheckedChanged" AutoPostBack="true" /> 
                                            </div>

                                        </td>
                                </tr>
                                <tr id="status" runat="server" visible="false">
                                    <td>Shipping Service&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="drpshipservi" runat="server" CssClass="form-control select2" >
                                                        <asp:ListItem Value="0" Selected="True"> ---Select--- </asp:ListItem>
                                                        <asp:ListItem Value="1"> channel_commsion</asp:ListItem>
                                                        <asp:ListItem Value="2"> Channel_Gateway</asp:ListItem>
                                                        <asp:ListItem Value="3"> VL_Logistics</asp:ListItem>
                                                        <asp:ListItem Value="4"> VLPenalty</asp:ListItem>
                                                    </asp:DropDownList> </td>
                                     <td>Order&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="drporder" runat="server" CssClass="form-control select2" >
                                                        <asp:ListItem Value="0" Selected="True"> ---Select--- </asp:ListItem>
                                                        <asp:ListItem Value="1"> channel_commsion</asp:ListItem>
                                                        <asp:ListItem Value="2"> Channel_Gateway</asp:ListItem>
                                                        <asp:ListItem Value="3"> VL_Logistics</asp:ListItem>
                                                        <asp:ListItem Value="4"> VLPenalty</asp:ListItem>
                                                    </asp:DropDownList> </td>
                                </tr>
                                 <tr id="status2" runat="server" visible="false">
                                    <td>Transfer&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="drptransfer" runat="server" CssClass="form-control select2" >
                                                        <asp:ListItem Value="0" Selected="True"> ---Select--- </asp:ListItem>
                                                        <asp:ListItem Value="1"> channel_commsion</asp:ListItem>
                                                        <asp:ListItem Value="2"> Channel_Gateway</asp:ListItem>
                                                        <asp:ListItem Value="3"> VL_Logistics</asp:ListItem>
                                                        <asp:ListItem Value="4"> VLPenalty</asp:ListItem>
                                                    </asp:DropDownList> </td>
                                     <td>Refund&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="drprefund" runat="server" CssClass="form-control select2" >
                                                        <asp:ListItem Value="0" Selected="True"> ---Select--- </asp:ListItem>
                                                        <asp:ListItem Value="1"> channel_commsion</asp:ListItem>
                                                        <asp:ListItem Value="2"> Channel_Gateway</asp:ListItem>
                                                        <asp:ListItem Value="3"> VL_Logistics</asp:ListItem>
                                                        <asp:ListItem Value="4"> VLPenalty</asp:ListItem>
                                                    </asp:DropDownList> </td>
                                </tr>
                                 

                                 <tr>
                                    <td>Channel Commission
                                       
                                        <asp:Label ID="lblchannel_commsion" runat="server" Text="channel_commsion" Visible="false"></asp:Label> 
                                    </td>
                                    <td> <div class="col-md-6"> 
                                        <asp:TextBox ID="txtchannel_commsion" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6"> 
                                        Channel Commission is included of GST &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkchnelcomm" runat="server"  /> </div></td>
                                </tr>
                                 <tr>
                                    <td>Channel Gateway
                                        
                                        <asp:Label ID="lblChannel_Gateway" runat="server" Text="Channel_Gateway" Visible="false"></asp:Label> 
                                    </td>
                                    <td><div class="col-md-6"> 
                                        <asp:TextBox ID="txtChannel_Gateway" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6"> 
                                        Channel Gateway is included of GST &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chchngate" runat="server"  /></div></td>
                                </tr>
                                 <tr>
                                    <td>Channel Logistic
                                        
                                        <asp:Label ID="lblVL_Logistics" runat="server" Text="VL_Logistics" Visible="false"></asp:Label> 
                                    </td>
                                    <td><div class="col-md-6">
                                        <asp:TextBox ID="txtVL_Logistics" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                        Channel Logistic is included of GST &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chklogis" runat="server"  />
                                        </div>
                                        </td>
                                </tr>
                                 <tr>
                                    <td>Channel Penalty
                                       
                                        <asp:Label ID="lblVLPenalty" runat="server" Text="VLPenalty" Visible="false"></asp:Label> 
                                    </td>
                                    <td><div class="col-md-6"> 
                                        <asp:TextBox ID="txtVLPenalty" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                        Channel Penalty is included of GST &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkpenlty" runat="server"  /></div></td>
                                </tr>
                                 <tr>
                                    <td>Pack Charges VL_Misc
                                       
                                        <asp:Label ID="lblPack_Charges_VL_Misc" runat="server" Text="Pack_Charges_VL_Misc" Visible="false"></asp:Label> 
                                    </td>
                                    <td> <div class="col-md-6">
                                         <asp:TextBox ID="txtPack_Charges_VL_Misc" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                         <div class="col-md-6">
                                        Pack Charges VL_Misc is included of GST &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkmic1" runat="server"  /></div></td>
                                </tr>
                                <tr>
                                    <td>Special Pack Charges Channel Mics
                                       

                                        <asp:Label ID="lblSpcl_pack_chrgs_vlMics" runat="server" Text="Spcl_pack_chrgs_vlMics" Visible="false"></asp:Label> 
                                    </td>
                                    <td><div class="col-md-6">
                                         <asp:TextBox ID="txtSpcl_pack_chrgs_vlMics" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                        Special Pack Charges Channel Mics is included of GST &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkmic2" runat="server"  /></div></td>
                                </tr>
                                <tr>
                                    <td>MP Commission CGST
                                        <asp:Label ID="lblmp_commission_cgst" runat="server" Text="mp_commission_cgst" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtmp_commission_cgst" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Payment Gateway Commission CGST
                                        <asp:Label ID="lblpg_commission_cgst" runat="server" Text="pg_commission_cgst" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtpg_commission_cgst" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Logistic CGST
                                        <asp:Label ID="lbllogistics_cgst" runat="server" Text="logistics_cgst" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtlogistics_cgst" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>TCS CGST
                                        <asp:Label ID="lblTCS_CGST" runat="server" Text="TCS_CGST" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtTCS_CGST" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>MP Commission IGST
                                        <asp:Label ID="lblmp_commission_igst" runat="server" Text="mp_commission_igst" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtmp_commission_igst" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td>Payment Gateway Commission IGST
                                        <asp:Label ID="lblpg_commission_igst" runat="server" Text="pg_commission_igst" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtpg_commission_igst" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td>Logistic IGST
                                        <asp:Label ID="lbllogistics_igst" runat="server" Text="logistics_igst" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtlogistics_igst" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td>TCS IGST
                                        <asp:Label ID="lblTCS_IGST" runat="server" Text="TCS_IGST" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtTCS_IGST" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                
                                <tr>
                                    <td>MP Commission SGST
                                        <asp:Label ID="lblmp_commission_sgst" runat="server" Text="mp_commission_sgst" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtmp_commission_sgst" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Payment Gateway Commission SGST
                                        <asp:Label ID="lblpg_commission_sgst" runat="server" Text="pg_commission_sgst" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtpg_commission_sgst" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Logistic SGST
                                        <asp:Label ID="lbllogistics_sgst" runat="server" Text="logistics_sgst" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtlogistics_sgst" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>TCS SGST
                                        <asp:Label ID="lblTCS_GST" runat="server" Text="TCS_GST" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtTCS_GST" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Total Cgst
                                        <asp:Label ID="lblTotal_Cgst" runat="server" Text="Total_Cgst" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtTotal_Cgst" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td>Total IGST
                                        <asp:Label ID="lblTotal_Igst" runat="server" Text="Total_Igst" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtTotal_Igst" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td>Total SGST
                                        <asp:Label ID="lblTotal_Sgst" runat="server" Text="Total_Sgst" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtTotal_Sgst" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td>Total Tax Liable GST(Before Adjusting TCS)
                                        
                                        <asp:Label ID="lbltotalsales_taxliable_gstbeforeadustingTCS" runat="server" Text="totalsales_taxliable_gstbeforeadustingTCS"  Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txttotalsales_taxliable_gstbeforeadustingTCS" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td>Payment Status
                                        <asp:Label ID="lblPayment_Status" runat="server" Text="Payment_Status" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtPayment_Status" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                
                                <tr>
                                    <td>Merchant SKU
                                        <asp:Label ID="lblMerchant_SKU" runat="server" Text="Merchant_SKU" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtMerchant_SKU" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Settlement Date
                                        <asp:Label ID="lblSettlement_Date" runat="server" Text="Settlement_Date" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtSettlement_Date" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Payment Type
                                        <asp:Label ID="lblPayment_Type" runat="server" Text="Payment_Type" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtPayment_Type" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Payable Amount
                                        <asp:Label ID="lblPayable_Amoun" runat="server" Text="Payable_Amoun" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtPayable_Amoun" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>PG UTR
                                        <asp:Label ID="lblPG_UTR" runat="server" Text="PG_UTR" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtPG_UTR" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td>Packaging Fees For Returns
                                        <asp:Label ID="lblpackfee_for_return" runat="server" Text="packfee_for_return" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtpackfee_for_return" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td>Special Packaging Fees For Returns
                                       <asp:Label ID="lblspcl_packfee_for_return" runat="server" Text="spcl_pack_fee_for_return" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtspcl_packfee_for_return" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td>Payment Collection Fees For Returns
                                        
                                        <asp:Label ID="lblpymtfee_for_return" runat="server" Text="pymtfee_for_return" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtpymtfee_for_return" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td>Logistics Fees For Returns
                                        <asp:Label ID="lbllogistfee_for_return" runat="server" Text="logistfee_for_return" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtlogistfee_for_return" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                
                                <tr>
                                    <td>Reverse Logistics Fees For Returns
                                        <asp:Label ID="lblreverse_logistfee_for_return" runat="server" Text="reverse_logistfee_for_return" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtreverse_logistfee_for_return" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Order Date
                                        <asp:Label ID="lblorder_date" runat="server" Text="order_date" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtorder_date" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td>Clossing Fees
                                        <asp:Label ID="lblclosing_fees" runat="server" Text="closing_fees" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtclosing_fees" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td>Other Charges
                                        <asp:Label ID="lblother_charges" runat="server" Text="other_charges" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtother_charges" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td>miscellaneous 
                                        <asp:Label ID="lblMisc_charge" runat="server" Text="Misc_charge" Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtMisc_charge" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td>Product ID 
                                        <asp:Label ID="productid" runat="server" Text="Product ID " Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtproductid" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                 <tr>
                                    <td>Product Name 
                                        <asp:Label ID="lbltxtproductname" runat="server" Text="Product Name " Visible="false"></asp:Label> 
                                    </td>
                                    <td><asp:TextBox ID="txtproductname" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                              

                            </tbody>
                                <tfoot>
                            </tfoot>
                        </table>
                           
                   
                    </div>
                    <!-- /.box-body -->
                </div>
   
                <!-- /.box -->
            </div>
            <!-- /.col -->
        </div>
                         <div class="box-footer btn-toolbar " style="margin-left: 0px;" runat="server" id="divupdatepytmsett" visible="false">
                        
                        <asp:Button ID="btnupdatepymtset"  class="btn btn-warning pull-right" runat="server" Text="Update Setting" OnClick="btnupdatepymtset_Click" />
                        <asp:Button ID="btnback" class="btn btn-danger pull-right" runat="server" Text="Back" OnClick="btnback_Click" />
                    </div>
                    </div>
                    

                </div>
                <div class="box-footer btn-toolbar " style="margin-left: 0px;" runat="server" id="devsavehideshow" visible="false">
                        <asp:Button ID="btnSave" Visible="false" class="btn btn-success pull-right" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnUpdate" Visible="false" class="btn btn-warning pull-right" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnCancel" class="btn btn-danger pull-right" runat="server" Text="Back" OnClick="btnCancel_Click" CausesValidation="False" />
                    </div>

                <section class="content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box">
                                <div class="box-header">
                                    <h3 class="box-title">
                                         
                                        <asp:Button ID="btnAdd" runat="server" Text="Add New" class="btn btn-primary" OnClick="btnAdd_Click" /></h3>

                                    <div class="box-body table-responsive" style="border: none !important">
                                        <div id="divAddAlert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Succesfully Added</h5>

                                        </div>

                                        <div id="divUpdAlert" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Succesfully Updated</h5>

                                        </div>

                                    </div>

                                </div>
                                <div class="box-body">
                                    <table id="" class="table table-bordered table-striped dtSearch">
                                        <thead>
                                            <tr>
                                                <asp:HiddenField ID="hdnID" runat="server" />
                                                <th>ID</th>
                                                <th>Location</th>
                                                <th>Location Type</th>
                                                <th>Location Type2</th>
                                                <th>Contact</th>
                                                <th>Address</th>
                                                <th>Add</th>
                                                <th>Payment Setting</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater ID="GV" runat="server" OnItemCommand="rptr_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("LocationID")%> 
                                                             <asp:Label ID="lbllocationid" runat="server" Text='<%# Eval("LocationID").ToString() %>' Visible="false"></asp:Label>
                                                        </td>
                                                        <td><%# Eval("Location")%>
                                                            <asp:Label ID="lblnlocname" runat="server" Text='<%# Eval("Location").ToString() %>' Visible="false"></asp:Label>
                                                        </td>
                                                        <td><%# Eval("Name")%> </td>
                                                        <td><%# Eval("LT2NAME")%> </td>
                                                        <td><%# Eval("Contact")%> </td>
                                                        <td><%# Eval("Address")%> </td>
                                                        <td>
                                                            <asp:LinkButton ID="LinkButton1" CommandName="addsl" class="btn btn-primary" CausesValidation="False" CommandArgument='<%# Eval("LocationID") %>'
                                                                runat="server"><i class="icon-remove"></i>Settings</asp:LinkButton></td>
                                                         <td>
                                                            <asp:LinkButton ID="btnpaytmsetting" runat="server" OnClick="btnpaytmsetting_Click" CssClass="btn bg-olive btn-sm "><i class="fa fa-sliders"></i> Payment Setting</asp:LinkButton>
                                                        </td>

                                                        <td>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="edit"
                                                                ImageUrl="images/edit.png" CommandArgument='<%# Eval("LocationID") %>' />

                                                            <%-- <asp:ImageButton ID="ImageButton2" runat="server" CommandName="addsl"
                                                                ImageUrl="images/edit.png" CommandArgument='<%# Eval("LocationID") %>' />--%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

