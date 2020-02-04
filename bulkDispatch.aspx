<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="bulkDispatch.aspx.cs" Inherits="bulkDispatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">
        <section class="content-header">
            <h1>Bulk Dispatch
        <!--<small>it all starts here</small>-->
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Bulk Uploads</li>
                <li class="active">Bulk Dispatch</li>
            </ol>
        </section>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <section>
                    <div class="box">
                        <div class="box-body">
                            <!-- BOx -->
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Bulk Dispatch</h3>

                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">

                                    <div class="row">
                                        <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                        </div>
                                        <div id="divInsert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                        </div>
                                        <div id="divUpdate" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                        </div>
                                        <table class="table table-bordered">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="invid" AutoPostBack="true" runat="server" CssClass="form-control select2" DataTextField="dropName" DataValueField="invid"></asp:DropDownList>
                                                </td>

                                                <td>
                                                    <asp:LinkButton ID="searchBarcode" runat="server" CssClass="btn btn-sm btn-warning" OnClick="searchBarcode_Click"><i class="fa fa-search"></i> Search</asp:LinkButton>
                                                </td>

                                            </tr>

                                        </table>

                                        
                                 <table runat="server" id="tableHide" visible="false" class="table table-bordered table-striped" >
												
                                                <tr>
													<th>Customer Name</th>
													<td>
														<asp:TextBox ID="custname" runat="server" CssClass="form-control"></asp:TextBox></td>
												</tr>

												<tr>
													<th>Address 1</th>
													<td>
														<asp:TextBox ID="address1" runat="server" CssClass="form-control"></asp:TextBox></td>
												</tr>

												<tr>
													<th>Address 2</th>
													<td>
														<asp:TextBox ID="address2" runat="server" CssClass="form-control"></asp:TextBox></td>
												</tr>

												<tr>
													<th>City</th>
													<td>
														<asp:TextBox ID="city" runat="server" CssClass="form-control"></asp:TextBox></td>
												</tr>
                                                 <tr>
                                                     <th>Contact Number</th><td>
														<asp:TextBox ID="phoneNo" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                 </tr>
												<tr>
													<th>State</th>
													<td>
														<asp:DropDownList ID="stateID" runat="server" DataTextField="statename" DataValueField="stateid" CssClass="form-control select2"></asp:DropDownList></td>
												</tr>
                                     <tr>
													<th>Courier</th>
													<td>
														<asp:DropDownList ID="courier" runat="server" CssClass="form-control select2" DataTextField="courierName" DataValueField="courierId"></asp:DropDownList></td>
												</tr>
                                     <tr>
													<th>ABW No.</th>
													<td>
														<asp:TextBox ID="salesabwno" runat="server" CssClass="form-control"></asp:TextBox></td>
												</tr>
											</table>
                          
                                        <table class="table table-bordered table-stripped" >
                                            <tr>
                                                <th width="20%";>Change SP BY</th>
                                                <th>Percentage/Set Amount</th>
                                                <th>Apply</th>
                                            </tr>
                                            <tr>
                                                <td width="20%";><asp:RadioButtonList ID="changeSP" runat="server" RepeatDirection="Horizontal" CssClass="RadioButtonWidth ">
                                                    <asp:ListItem Value="1" Selected="True"> Percentage</asp:ListItem>
                                                    <asp:ListItem Value="2"> Set Amount</asp:ListItem>
                                                </asp:RadioButtonList></td>
                                                <td><asp:TextBox ID="searchField" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                <td><asp:LinkButton ID="changeSPAmount" runat="server" OnClick="changeSPAmount_Click" CssClass="btn btn-sm btn-warning"> Apply</asp:LinkButton></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                </td>
                                                <td><asp:LinkButton ID="changeSpbyExcel" runat="server" OnClick="changeSpbyExcel_Click" CssClass="btn btn-sm btn-info"> Apply</asp:LinkButton></td>
                                            </tr>
                                          
                                        </table>
                                        <table class="table table-bordered table-stripped" >
                                            <thead>
                                            <tr>
                                                <th><asp:CheckBox ID="checkSelectAll" runat="server" checked="true" AutoPostBack="true" OnCheckedChanged="checkSelectAll_CheckedChanged"/></th>
                                                <th>Barcode</th>
                                               
                                                <th>Title</th>
                                                <th>Category</th>
                                                <th>Sales ID</th>
                                                <th>SP</th>
                                                <th>Change SP</th>
                                            </tr>
                                                </thead>
                                            <tbody>
                                            <asp:Repeater ID="rpt_Barcode" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="checkBoxBarcode" runat="server" checked="true" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="StockupID" runat="server" Text='<%# Eval("StockupID").ToString() %>' Visible="false"></asp:Label>
                                                            
                                                            <asp:Label ID="sid" runat="server" Text='<%# Eval("sid").ToString() %>' Visible="false"></asp:Label>
                                                            
                                                            <asp:Label ID="barcodeNo" runat="server" Text='<%# Eval("barcodeNo").ToString() %>' Visible="false"></asp:Label>
                                                            <%# Eval("barcodeNo").ToString() %>
                                                        </td>
                                                        
                                                        <td><asp:Label ID="Title" runat="server" Text='<%# Eval("Title").ToString() %>' Visible="false"></asp:Label>
                                                            <%# Eval("Title").ToString() %>
                                                        </td>
                                                        <td><asp:Label ID="ItemCategory" runat="server" Text='<%# Eval("ItemCategory").ToString() %>' Visible="false"></asp:Label>
                                                           <%# Eval("ItemCategory").ToString() %>
                                                        </td>
                                                        <td><asp:Label ID="salesidgivenbyvloc" runat="server" Text='<%# Eval("salesidgivenbyvloc").ToString() %>' Visible="false"></asp:Label>
                                                            <%# Eval("salesidgivenbyvloc").ToString() %>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="actualSP" runat="server"  Text='<%# Eval("actualSP").ToString() %>' ></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="sellingprice" runat="server" CssClass="form-control" Text='<%# Eval("sellingprice").ToString() %>' ></asp:TextBox>
                                                            
                                                        </td>
                                                        
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            </tbody>
                                        </table>
                                       
                                          <div class="box-body table-responsive ">
														<div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                           <asp:Button ID="saveBulkDispatch" runat="server" Text="Dispatch" CssClass="btn btn-sm btn-success pull-right" OnClick="saveBulkDispatch_Click" OnClientClick="return confirm('Do you Want to Dispatch ?');" />
                                                            


														</div>
													</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </section>
            </ContentTemplate>
             <Triggers>
         <asp:PostBackTrigger ControlID="changeSpbyExcel" />
      </Triggers>
        </asp:UpdatePanel>

    </div>


</asp:Content>

