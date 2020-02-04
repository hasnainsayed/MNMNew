<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="invoice.aspx.cs" Inherits="invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper"> <section class="content-header">
        <h1>Invoice List
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active"><a href="invoice.aspx">Invoice List</a></li>
        </ol>
    </section>
           <asp:UpdatePanel ID="UpdatePanel3" runat="server">
           <ContentTemplate>
    <section class="content">
              <!-- Default box -->
        <div class="box">
            <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Invoice List</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                            <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>
                                    <div id="divSucc" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>
                             <asp:Panel ID="Panel3" runat="server" DefaultButton="searchSoldItem">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                            <table class="table table-bordered table-striped table-hover">
                                            <tr>
                                                <th width="20%";>Search By</th>
                                                <th runat="server" id="hSearch">Search Text</th>
                                                <th runat="server" id="hVloc" visible="false">Virtual Location</th>
                                                <th runat="server" id="hFDate" visible="false">From Date</th>
                                                <th runat="server" id="hTdate" visible="false">To Date</th>
                                                <th>Search</th>
                                            </tr>
                                            <tr>
                                                <td width="20%";>
                                                    <asp:DropDownList ID="searchBy" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="searchBy_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="1" Selected="True"> Barcode</asp:ListItem>
                                                        <asp:ListItem Value="2"> SalesID</asp:ListItem>
                                                        <asp:ListItem Value="3"> Virtual location</asp:ListItem>
                                                        <asp:ListItem Value="4"> Date Range</asp:ListItem>
                                                        <asp:ListItem Value="5"> Customer</asp:ListItem>
                                                        <asp:ListItem Value="6"> Invoice Status</asp:ListItem>
                                                    </asp:DropDownList> 
                                                    </td>
                                                <td runat="server" id="tSearch"><asp:TextBox ID="searchField" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                <td runat="server" id="tVloc" visible="false">
                                                    <asp:DropDownList ID="vLoc" runat="server" CssClass="form-control select2" DataTextField="Location" DataValueField="LocationID"></asp:DropDownList></td>
                                                <td runat="server" id="tFDate" visible="false">
                                                    <asp:TextBox ID="fromDate" CssClass="form-control datepicker" runat="server" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                                <td runat="server" id="tTdate" visible="false">
                                                    <asp:TextBox ID="toDate" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                                </td>
                                                <td><asp:LinkButton ID="searchSoldItem" runat="server" OnClick="searchSoldItem_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-search"></i> Search</asp:LinkButton></td>
                                            </tr>
                                <tr><td class="pull-right" colspan="3"><asp:LinkButton runat="server" ID="addPayments" CssClass="btn btn-sm btn-primary" OnClick="addPayments_Click"><i class="fa fa-plus"></i> Add Payments</asp:LinkButton></td>
                                    <td  colspan="3"><asp:LinkButton runat="server" ID="addTrader" CssClass="btn btn-sm btn-warning" OnClick="addTrader_Click"><i class="fa fa-plus"></i> Add Trader Notes</asp:LinkButton></td>

                                </tr>
                                        </table>
</ContentTemplate></asp:UpdatePanel>
                        </asp:Panel>
                            <table class="table table-bordered table-striped table-hover">
                                <thead>
                                <tr>
                                    <th width="15%">Invoice Number</th>
                                    <th>Invoice Status</th>
                                    <th>Customer</th>
                                    <th>Contact No</th>
                                    <th>State</th>
                                    <th>Total</th>
                                    <th>Payment Status</th>
                                    <th>Make Payment</th>
                                    <th>View</th>
                                    <th runat="server" visible="false">Cancel Invoice</th>
                                    <th>Mark Paid</th>
                                </tr>
                                    </thead>
                                <tbody>
                                <asp:Repeater ID="rpt_Invoice" runat="server" OnItemDataBound="rpt_Invoice_ItemDataBound">
                                   <ItemTemplate>
                                       <tr>
                                           <td>
                                               <asp:Label ID="invoiceid" runat="server" Visible="false" Text='<%# Eval("invid").ToString() %>' ></asp:Label>
                                               <%# Eval("invid").ToString() %>
                                           </td>
                                           <td><%# Eval("invoiceStatus").ToString() %></td>
                                           <td> <%# Eval("custname").ToString() %></td>
                                           <td> <%# Eval("phoneNo").ToString() %></td>
                                           <td> <%# Eval("statename").ToString() %></td>                                           
                                           <td> <%# Eval("total").ToString() %></td>
                                           <td> <%# Eval("paymentStatus").ToString() %></td>
                                           <td runat="server"><asp:LinkButton runat="server" ID="makePayment" Visible="false" CssClass="btn btn-bitbucket btn-sm" OnClick="makePayment_Click"><i class="fa fa-money"></i> Payment</asp:LinkButton></td>
                                           <td>
                                               <asp:LinkButton ID="viewInv" Visible="false" runat="server" OnClick="viewInv_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-eye"></i> View</asp:LinkButton></td>
                                            <td runat="server" visible="false">
                                                <asp:LinkButton runat="server" ID="cancelInvoice" OnClick="cancelInvoice_Click" Visible="false" CssClass="btn btn-sm btn-danger"><i class="fa fa-remove"></i> Cancel</asp:LinkButton></td>
                                            <td runat="server">
                                                <asp:LinkButton runat="server" ID="markPaid" OnClick="markPaid_Click" Visible="false" CssClass="btn btn-sm btn-primary"><i class="fa fa-credit-card"></i> Mark as Paid</asp:LinkButton></td>
                                       
                                       </tr>
                                       </ItemTemplate></asp:Repeater></tbody>
                            </table>
                            
                        
                        </div>

                        <div class="row">
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
						</div>
                        </div></div></div></div>
        </section>
                                    </ContentTemplate>
              
               </asp:UpdatePanel>

        </div>
</asp:Content>

