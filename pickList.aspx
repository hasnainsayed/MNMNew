<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pickList.aspx.cs" Inherits="pickList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper"> <section class="content-header">
        <h1>Pick List
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active"><a href="pickList.aspx">Pick List</a></li>
        </ol>
    </section>
           <asp:UpdatePanel ID="UpdatePanel3" runat="server">
           <ContentTemplate>
    <section class="content">
              <!-- Default box -->
        <div class="box">
            <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary" id="printPanel">
                    <div class="box-header with-border">
                        <h3 class="box-title">Pick List <span class="pull-right">
                            </span></h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div runat="server" id="pickListTable">
                        <div class="row">
                             <table class="table table-bordered">
                               <tr>
                                   <td>
                                       <asp:DropDownList ID="virtualLocation" AutoPostBack="true" runat="server" CssClass="form-control select2" DataTextField="Location" DataValueField="LocationID"></asp:DropDownList>
                                   </td>
                               
                                   <td>
                                       <asp:LinkButton ID="printPickList" runat="server" CssClass="btn btn-sm btn-success" OnClick="printPickList_Click"><i class="fa fa-print"></i> Print</asp:LinkButton>
                                   </td>
                                   
                               </tr>
                               
                           </table>

                            <table class="table table-bordered table-striped table-hover dtSearch">
                                <thead>
                                <tr>
                                    <th>Barcode</th>
                                    <th>Vertical</th>
                                    <th>Sub Category</th>
                                    <th>Virtual Location</th>
                                    <th>Brand</th>
                                    <th>Sales Id</th>
                                    <th>RackCode</th>
                                    <th>Image 1</th>
                                    <th>Dispatch</th>
                                    <th>Cancel</th>
                                </tr>
                                    </thead>
                                <tbody>
                                <asp:Repeater ID="rpt_PickList" runat="server" OnItemDataBound="rpt_PickList_ItemDataBound">
                                   <ItemTemplate>
                                       <tr>
                                           <td>
                                               <asp:Label ID="StockupID" runat="server" Visible="false" Text='<%# Eval("StockupID").ToString() %>' ></asp:Label>
                                               <asp:Label ID="sid" runat="server" Visible="false" Text='<%# Eval("sid").ToString() %>' ></asp:Label>
                                               <asp:Label ID="fullBarcode" runat="server" Text='<%# Eval("fullBarcode").ToString() %>' ></asp:Label>
                                               
                                           </td>
                                           
                                           <td> <asp:Label ID="ItemCategory" runat="server" Text='<%# Eval("ItemCategory").ToString() %>' ></asp:Label></td>
                                           <td> <asp:Label ID="subCategory" runat="server" Text='<%# Eval("C2Name").ToString() %>' ></asp:Label></td>
                                           <td> <asp:Label ID="vLoc" runat="server" Text='<%# Eval("Location").ToString() %>' ></asp:Label></td>
                                           <td> <asp:Label ID="C1Name" runat="server" Text='<%# Eval("C1Name").ToString() %>' ></asp:Label></td>
                                           <td> <asp:Label ID="salesidgivenbyvloc" runat="server" Text='<%# Eval("salesidgivenbyvloc").ToString() %>' ></asp:Label></td>
                                           <td> <asp:Label ID="RackBarcode" runat="server" Text='<%# Eval("RackBarcode").ToString() %>' ></asp:Label></td>
                                           <td> 
                                               <asp:Label ID="image1" runat="server" Visible="false" Text='<%# Eval("image1").ToString() %>' ></asp:Label>
                                               <asp:image ID="image1Display" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/>

                                           </td>
                                           <td> <asp:LinkButton ID="dispatchItem" runat="server" OnClick="dispatchItem_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-truck"></i> Dispatch</asp:LinkButton></td>
                                           <td> <asp:LinkButton ID="cancleItem" runat="server" OnClick="cancleItem_Click" CssClass="btn btn-sm btn-danger"><i class="fa fa-trash"></i> Cancel</asp:LinkButton></td>
                                       </tr>
                                       </ItemTemplate></asp:Repeater></tbody>
                                <tfoot></tfoot>
                            </table>
                            </div>
                        </div>

                        <div runat="server" id="dispatchtable" visible="false">
                            
                        	<div class="row">
							<div class="col-xs-12">
								<div class="box">
									<div class="box-body table-responsive">
                                        <div class="col-xs-12">
                                            <table class="table table-bordered">
                                                <tr>
                                   <th>Barcode</th>
                                    <th>Vertical</th>
                                    <th>Sub category</th>
                                    <th>Virtual Location</th>
                                    <th>Brand</th>
                                    <th>Sales Id</th>
                                    <th>RackCode</th>
                                    <th>Image 1</th>
                                   
                                   
                                </tr>
                                                <tr>
                                                    <td><asp:Label ID="displayBarcode" runat="server" Text=""></asp:Label></td>
                                                    <td><asp:Label ID="displayItemCategory" runat="server" Text=""></asp:Label></td>
                                                    <td><asp:Label ID="displaySubCategory" runat="server" Text=""></asp:Label></td>
                                                    <td><asp:Label ID="displayVirtual" runat="server" Text=""></asp:Label></td>
                                                    <td><asp:Label ID="displayC1Name" runat="server" Text=""></asp:Label></td>
                                                    <td><asp:Label ID="displaysalesidgivenbyvloc" runat="server" Text=""></asp:Label></td>
                                                    
                                                    <td><asp:Label ID="displayRackCode" runat="server" Text=""></asp:Label></td>
                                                    <td><asp:image ID="displayimage1Display" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/></td>
                                                </tr>
                                            </table>
                                           <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                    </div>
                                            <asp:Label ID="displayStockupID" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="displaysid" runat="server" Text="" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Courier Service </label>
                                            <asp:DropDownList ID="courier" runat="server" CssClass="form-control select2" DataTextField="courierName" DataValueField="courierId"></asp:DropDownList>
                                        </div>

                                        </div>
                                        <div class="col-md-6">
                                        <div class="form-group">
                                            <label>AWB No. </label>
                                            <asp:TextBox ID="awbNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        
                                        </div>
                                        <div class="col-md-12">
                                        <table class="table table-bordered table-striped">
												
												<asp:Label runat="server" Visible="false" ID="invoiceId"></asp:Label>
												
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
													<th>Contact Number</th>
													<td>
														<asp:TextBox ID="phoneNo" runat="server" CssClass="form-control"></asp:TextBox></td>
												</tr>
												<tr>
													<th>City</th>
													<td>
														<asp:TextBox ID="city" runat="server" CssClass="form-control"></asp:TextBox></td>
												</tr>

												<tr>
													<th>State</th>
													<td>
														<asp:DropDownList ID="stateID" runat="server" DataTextField="statename" DataValueField="stateid" CssClass="form-control select2"></asp:DropDownList></td>
												</tr>
											</table>
                                        </div>

                                                  <div class="box-body table-responsive ">
                                                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                        <asp:Button ID="btnPickList"  class="btn btn-danger pull-right btn-sm" runat="server" Text="Return to Pick List" OnClick="btnPickList_Click"/>
                                                        <asp:Button ID="btnSave"  class="btn btn-success pull-right btn-sm" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="grp"/>
                                                        
                                                    </div>
                                                </div>
                                        </div>
                                    </div>
                                </div>
                                </div>
                        </div>

                        <div runat="server" id="cancleTable" visible="false">
                            
                        	<div class="row">
							<div class="col-xs-12">
								<div class="box">
									<div class="box-body table-responsive">
                                        <div class="col-xs-12">
                                            <table class="table table-bordered">
                                                <tr>
                                   <th>Barcode</th>
                                    <th>Vertical</th>
                                    <th>Sub category</th>
                                    <th>Virtual Location</th>
                                    <th>Brand</th>
                                    <th>Sales Id</th>
                                    <th>RackCode</th>
                                    <th>Image 1</th>
                                   
                                   
                                </tr>
                                                <tr>
                                                    <td><asp:Label ID="displayBarcode1" runat="server" Text=""></asp:Label></td>
                                                    <td><asp:Label ID="displayItemCategory1" runat="server" Text=""></asp:Label></td>
                                                    <td><asp:Label ID="displaySubCategory1" runat="server" Text=""></asp:Label></td>
                                                    <td><asp:Label ID="displayVirtual1" runat="server" Text=""></asp:Label></td>
                                                    <td><asp:Label ID="displayC1Name1" runat="server" Text=""></asp:Label></td>
                                                    <td><asp:Label ID="displaysalesidgivenbyvloc1" runat="server" Text=""></asp:Label></td>
                                                    
                                                    <td><asp:Label ID="displayRackCode1" runat="server" Text=""></asp:Label></td>
                                                    <td><asp:image ID="displayimage1Display1" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/></td>
                                                </tr>
                                            </table>
                                           <div id="divError1" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                    </div>
                                            <asp:Label ID="displayStockupID1" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="invoiceId1" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="displaysid1" runat="server" Text="" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Status </label>
                                            <asp:DropDownList ID="cancleStatus" runat="server" CssClass="form-control select2">
                                                                    <asp:ListItem Value="RFL">RFL</asp:ListItem>
                                                                    <asp:ListItem Value="NRM">Minor</asp:ListItem>
                                                                    <asp:ListItem Value="NRD">Damaged</asp:ListItem>
                                                                    <asp:ListItem Value="NRR">Retail</asp:ListItem>
                                                                </asp:DropDownList>
                                        </div>

                                        </div>
                                          <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Status </label>
                                            <asp:DropDownList ID="cancleReason" runat="server" CssClass="form-control select2">
                                                                    <asp:ListItem Value="Customer Cancellation">Customer Cancellation</asp:ListItem>
                                                                    <asp:ListItem Value="Self Cancellation">Self Cancellation</asp:ListItem>
                                                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                                                   
                                                                </asp:DropDownList>
                                        </div>

                                        </div>
                                                         <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Reason </label>
                                         <asp:TextBox runat="server" CssClass="form-control" ID="reasons"></asp:TextBox>
                                        </div>

                                        </div>
                                       
                                        <div class="col-md-12">
                                        
                                        </div>

                                                  <div class="box-body table-responsive ">
                                                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                        <asp:Button ID="btnRetPick"  class="btn btn-danger pull-right btn-sm" runat="server" Text="Return to Pick List" OnClick="btnPickList_Click"/>
                                                        <asp:Button ID="btnCanclePickList"  class="btn btn-success pull-right btn-sm" runat="server" Text="Cancel Item" OnClientClick="return confirm('Do You Want to Cancel Item ? It will be deleted from Sales Record');" OnClick="btnCanclePickList_Click"/>
                                                        
                                                    </div>
                                                </div>
                                        </div>
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

