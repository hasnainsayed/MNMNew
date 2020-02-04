﻿
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RemittanceRepo.aspx.cs" Inherits="RemittanceRepo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper"> <section class="content-header">
        <h1> Ramittence Report 
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li>Ramittence Report </li>
            <li class="active"> Ramittence Report </li>
        </ol>
    </section>

     <asp:UpdatePanel ID="UpdatePanel20" runat="server">
            <ContentTemplate>
            <section class="content">
              <!-- Default box -->
        <div class="box">
            <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary">
                    <table class="table table-bordered">
                               <tr>
                                   <td>
                                       <asp:DropDownList ID="virtualLocation" AutoPostBack="true" runat="server" CssClass="form-control select2" DataTextField="Location" DataValueField="LocationID"></asp:DropDownList>
                                   </td>
                                 
                                   <td>
                                       <asp:LinkButton ID="btnsearch" runat="server" CssClass="btn btn-primary pull-left" OnClick="btnsearch_Click"><i class="fa fa-search"></i> Search</asp:LinkButton>
                                   </td>
                                  
                                   
                               </tr>
                               
                           </table>
                    <div runat="server" id="maindiv" visible="false" class=" table-responsive">
                    <div class="box-header with-border">
                         <div class="box-header with-border">
                        <h3 class="box-title"><asp:LinkButton ID="btnexporttoexcel" runat="server" CssClass="btn btn-success pull-right btn-round" OnClick="btnexporttoexcel_Click"><i class="fa  fa-file-excel-o"></i> Export</asp:LinkButton></h3>

                    </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body table-responsive">
                       <%-- <div class="row">--%>
                            <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>
                                    <div id="divSucc" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>
                             
                              <asp:Label ID="lblsalesprice" runat="server" Text='<%# Eval("MRP").ToString() %>' Visible="false"></asp:Label>
                              <asp:Label ID="lbltotalingst" runat="server" Text='<%# Eval("totalincludingst").ToString() %>' Visible="false"></asp:Label>
                            <%--<div class="box-body >--%>
                            <table class="table table-bordered table-striped table-hover dtSearch">
                              
                       
                                <thead>
                                            <tr>
                                                <th>Pmt_Date</th>
                                                 <th>Pmt_Month</th>
                                                 <th>Pmt_Year</th>
                                                <th>Sales Date</th>
                                                 <th>Sales Month</th>
                                                 <th>Sales Year</th>
                                                 <th>Channel(VL)</th>
                                                 <th>Invoice #</th>
                                                 <th>Order id#</th>
                                                 <th>StockUp id#</th>
                                                 <th>Payment Status</th>
                                                 <th>Pmt Effecting Status</th>
                                                 <th>System Status</th>
                                                 <th>Lot#</th>
                                                 <th>Brand</th>
                                                 <th>Title</th>
                                                 <th>Size</th>
                                                 <th>Article</th>
                                                 <th>Age Bucket (Lot)</th>
                                                 <th>Age Bucket (Barcode)</th>
                                                 <th>MRP</th>
                                                 <th>CP</th>
                                                 <th>Pmt Sales Price</th>
                                                 <th>System Sales Price</th>
                                                 <th>Channel(VL) Commision</th>
                                                 <th>Channel Gateway</th>
                                                 <th>Channel Logistics</th>
                                                 <th>VL Penalty</th>
                                                 <th>VL Misc</th>
                                                 <th>Total  IGST</th>
                                                 <th>Total SGST</th>
                                                 <th>Total CSGT</th>
                                                 <th>Total Deduction (inc GST)</th>
                                                 <th>Total Deduction (exl GST)</th>
                                                 <th>Settled Amount(inc GST)</th>
                                                 <th>Settled %(inc GST)</th>
                                                  <th>Settled Amount(exl GST)</th>
                                                 <th>Settled %(exl GST)</th>
                                                 <th>Net profit</th>
                                                 <th>Net Profit%</th>
                                                
                                           </tr>
                                    </thead>
                                <tbody>
                                            <asp:Repeater ID="rpt_salesidnotexits" runat="server" OnItemDataBound="rpt_salesidnotexits_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                          <asp:Label ID="IGST" runat="server" Text='<%# Eval("totIGST").ToString() %>' Visible="false"></asp:Label> 
                                                          <asp:Label ID="SGST" runat="server" Text='<%# Eval("totSGST").ToString() %>' Visible="false"></asp:Label> 
                                                          <asp:Label ID="CGSt" runat="server" Text='<%# Eval("totCGST").ToString() %>' Visible="false"></asp:Label> 
                                                          <asp:Label ID="selling_price" runat="server" Text='<%# Eval("sys_sp").ToString() %>' Visible="false"></asp:Label> 
                                                          <asp:Label ID="cp" runat="server" Text='<%# Eval("cp").ToString() %>' Visible="false"></asp:Label> 
                                                         
                                                         <td><%# Eval("pmt_date")%></td>
                                                         <td><%# Eval("pmt_Month")%></td>
                                                         <td><%# Eval("pmt_year")%></td>
                                                         <td><%# Eval("date")%></td>
                                                         <td><%# Eval("Month")%></td>
                                                         <td><%# Eval("year")%></td>
                                                         <td><%# Eval("Location")%></td>
                                                         <td><%# Eval("invoiceid")%></td>
                                                         <td><%# Eval("salesid")%></td>
                                                         <td><%# Eval("stockupId")%></td>
                                                         <td><%# Eval("Payment_Status")%></td>
                                                         <td><%# Eval("pmt_effecting_status")%></td>
                                                         <td><%# Eval("sys_status")%></td>
                                                         <td><%# Eval("LotNo")%></td>
                                                         <td><%# Eval("Brand")%></td>
                                                         <td><%# Eval("Title")%></td>
                                                         <td><%# Eval("Size")%></td>
                                                         <td><%# Eval("articel")%></td>
                                                         <td><%# Eval("lotAge")%></td>
                                                         <td><%# Eval("stockAge")%></td>
                                                         <td><%# Eval("mrp")%></td>
                                                         <td><%# Eval("cp")%></td>
                                                         <td><%# Eval("pmt_sp")%></td>
                                                         <td><%# Eval("sys_sp")%></td>
                                                         <td><%# Eval("channel_commsion")%></td>
                                                         <td><%# Eval("Channel_Gateway")%></td>
                                                         <td><%# Eval("VL_Logistics")%></td>
                                                         <td><%# Eval("VLPenalty")%></td>
                                                         <td><%# Eval("misccharges")%></td>
                                                         <td><%# Eval("totIGST")%></td>
                                                         <td><%# Eval("totSGST")%></td>
                                                         <td><%# Eval("totCGST")%></td>
                                                        <td><asp:Label ID="lbltotdecinc" runat="server" Text=""></asp:Label> </td>
                                                        <td><asp:Label ID="lbltotdecexc" runat="server" Text=""></asp:Label> </td>
                                                        <td><asp:Label ID="setamtinc" runat="server" Text=""></asp:Label> </td>
                                                        <td><asp:Label ID="setperinc" runat="server" Text=""></asp:Label> </td>
                                                        <td><asp:Label ID="setamtexc" runat="server" Text=""></asp:Label> </td>
                                                        <td><asp:Label ID="setperexc" runat="server" Text=""></asp:Label> </td>
                                                        <td><asp:Label ID="netpro" runat="server" Text=""></asp:Label> </td>
                                                        <td><asp:Label ID="lblnetper" runat="server"  Text=""></asp:Label> </td>
                                                         <%--<td><%# Eval("totDeduction")%></td>
                                                         <td></td>
                                                         <td><%# Eval("setamt")%></td>
                                                         <td> <%# Eval("setper")%></td>
                                                         <td> <%# Eval("netpro")%></td>
                                                         <td> <%# Eval("netper")%></td>--%>
                                                           
                                                       
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                              </tbody>
                                <tfoot></tfoot>
                                        </table>
                                <%-- </div>--%>
                             </div>
                             </div>
                    </div>
                        </div>
                    </div>
              <%--  </div>--%>
        <%--   <div class="row">
                    <div class="col-xs-12">
                        <div>
                            <div class="box-body table-responsive ">
                                <table class="table table-bordered table-stripped">
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="lbFirst" runat="server" CssClass=""
                                                OnClick="lbFirst_Click">First</asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbPrevious" runat="server"
                                                OnClick="lbPrevious_Click">Previous</asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:DataList ID="rptPaging" runat="server"
                                                OnItemCommand="rptPaging_ItemCommand"
                                                OnItemDataBound="rptPaging_ItemDataBound"
                                                RepeatDirection="Horizontal">
                                                <ItemTemplate>
                                                    <asp:Button class="btn btn-success" BackColor="#999999" BorderColor="#999999" ID="lbPaging" runat="server"
                                                        CommandArgument='<%# Eval("PageIndex") %>'
                                                        CommandName="newPage"
                                                        Text='<%# Eval("PageText") %> '></asp:Button>
                                                    &nbsp;
                                            
                                            
                                           
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>

                                        <td>
                                            <asp:LinkButton ID="lbNext" runat="server"
                                                OnClick="lbNext_Click">Next</asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbLast" runat="server"
                                                OnClick="lbLast_Click">Last</asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblpage" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>

                    </div>
                </div>--%>
                           <%-- </div>           --%>       
                       </section>
               
            <%--</div>--%>
                </ContentTemplate>
         <Triggers>
             <asp:PostBackTrigger ControlID="btnexporttoexcel" />
            
         </Triggers>
         </asp:UpdatePanel>
         </div>
</asp:Content>

