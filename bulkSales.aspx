<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="bulkSales.aspx.cs" Inherits="bulkSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper"> <section class="content-header">
        <h1>Bulk Sales-Scanning
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li>Bulk Uploads</li><li>Bulk Sales</li>
            <li class="active">Bulk Sales-Scanning</li>
        </ol>
    </section>
   
                                    <section>
                                           <div class="box">
            <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Bulk Sales-Scanning</h3>

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
                             
                           	<table class="table table-bordered table-striped">
												<tr>
													<th>Virtual location</th>
													<td>
														<%--<asp:Label ID="lblStyleID" runat="server" Text="" visible="false"></asp:Label>--%>
														<asp:Label ID="lblGSTPercent" runat="server" Text="" Visible="false"></asp:Label>
														<asp:DropDownList ID="virtualLocation" runat="server" DataTextField="Location" DataValueField="locationID" AutoPostBack="true" CssClass="form-control select2"></asp:DropDownList></td>
												        <th>SalesID</th>
													<td>
														<asp:TextBox ID="salesId" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                </tr>
												
												<tr>
													<th>Customer Name</th>
													<td>
														<asp:TextBox ID="custname" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                    <th>Address 1</th>
													<td>
														<asp:TextBox ID="address1" runat="server" CssClass="form-control"></asp:TextBox></td>
												</tr>

												<tr>
													<th>Address 2</th>
													<td>
														<asp:TextBox ID="address2" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                    <th>City</th>
													<td>
														<asp:TextBox ID="city" runat="server" CssClass="form-control"></asp:TextBox></td>
												</tr>

												<tr>
													<th>State</th>
													<td>
														<asp:DropDownList ID="stateID" runat="server" DataTextField="statename" DataValueField="stateid" CssClass="form-control select2"></asp:DropDownList></td>
												<th>Payment Mode</th>
													<td>
                                                        <asp:RadioButtonList ID="paymentMode" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="cod">COD</asp:ListItem>
                                                            <asp:ListItem Value="Prepaid">Prepaid</asp:ListItem>
                                                        </asp:RadioButtonList>
													</td>
                                                    
                                                </tr>

                                                <tr>
                                                    <th>Sales Date</th>
                                                    <td>
                                                        <asp:TextBox ID="salesDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                    </td><th>Contact Number</th><td>
														<asp:TextBox ID="phoneNo" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                </tr>
                                   <asp:Panel runat="server" DefaultButton="btnAddToRepeater">
                      
                                   <tr>
                                       <th>
                                           Barcode
                                       </th>
                                       <td colspan="2">
                                           <asp:TextBox ID="barcodeTxt" runat="server" CssClass="form-control" ></asp:TextBox></td>
                                       <td>
                                           
                                           <asp:Button ID="btnAddToRepeater" runat="server" OnClick="btnAddToRepeater_Click" CssClass="btn btn-primary btn-sm" Text="Add"></asp:Button>
                                       </td>
                                   </tr>
                                   </asp:Panel>

                                   <tr>
                                       <th>Upload Excel</th>
                                       <td colspan="2">
                                           <asp:FileUpload ID="FileUpload1" runat="server" />
                                       </td>
                                       <td>
                                           <asp:Button ID="uploadExcel" runat="server" Text="Upload Excel" CssClass="btn btn-sm btn-danger" OnClick="uploadExcel_Click"/></td>
                                   </tr>
											</table>
                             
                             <table class="table table-bordered table-striped table-hover dtSearch">
                                 <thead>
                                 <tr>
                                     <th>Barcode : <asp:Label runat="server" id="totalBarcodes" style="color:red"></asp:Label></th>
                                   
                                     <th>Remove</th>
                                 </tr>
                                </thead>
                                 <tbody>
                                 <asp:Repeater runat="server" ID="rpt_Barcode">
                                     <ItemTemplate>
                                            
                                                 <tr runat="server" id="trBarcode">
                                                     <td><asp:Label ID="barcode" runat="server" Text='<%# Eval("barcode") %>'></asp:Label></td>
                                                     <td><asp:LinkButton ID="removeBarcode" runat="server" CssClass="tab-danger" OnClientClick="return confirm('Do You Want to Remove Barcode ?');" OnClick="removeBarcode_Click"><i class="fa fa-trash"></i></asp:LinkButton></td>
                                                 </tr>
                                             
                                     </ItemTemplate>
                                 </asp:Repeater>
                                 </tbody>
                             </table>
                             <div class="box-body table-responsive ">
														<div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                           <asp:Button ID="saveBulkSales" runat="server" Text="Save" CssClass="btn btn-sm btn-success pull-right" OnClick="saveBulkSales_Click" OnClientClick="return confirm('Do you Want to Save ?');" />
                                                            


														</div>
													</div>

                            </div>
                        </div>
                    </div>
                </div>
                                               </div>
                                        
                                    </section>
                                 
        </div>
</asp:Content>

