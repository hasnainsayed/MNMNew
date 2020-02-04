<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewInventory.aspx.cs" Inherits="ViewInventory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %><%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="content-wrapper">
		<section class="content-header">
			<h1>View Inventory
			</h1>
			<ol class="breadcrumb">
				<li><a href="index.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
				<li class="active">Master</li>
				<li class="active">View Inventory</li>

			</ol>
		</section>
		<section class="content">

			<asp:UpdatePanel ID="UpdatePanel14" runat="server">
				<ContentTemplate>
                    <!-- Default box -->
        <div class="box">
            <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Item Style Search</h3>

                       
                    </div>
                    <!-- /.box-header -->   
                    <asp:Panel ID="Panel3" runat="server" DefaultButton="btnShowAll">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                        <div class="row">
                           
                         <table class="table table-bordered">
                             <tr>
                                 <th>
                                     Vertical
                                 </th>
                                 <th>
                                     <asp:Label ID="lblCat2" runat="server" Text=""></asp:Label>
                                 </th>
                                 <th>
                                     <asp:Label ID="lblCat3" runat="server" Text=""></asp:Label>
                                 </th>
                                 <th>
                                     <asp:Label ID="lblCat4" runat="server" Text=""></asp:Label>
                                 </th>
                                 <th>
                                     <asp:Label ID="lblCat5" runat="server" Text=""></asp:Label>
                                 </th>
                                 <th>Style Code</th>
                             </tr>
                             <tr id="showCat" runat="server">
                                 <td>
                                     <asp:DropDownList ID="drp_itemCategory" runat="server" AutoPostBack="true" DataValueField="ItemCategoryID" DataTextField="ItemCategory" CssClass="form-control select2" OnSelectedIndexChanged="drp_itemCategory_SelectedIndexChanged"></asp:DropDownList>
                                 </td>
                                 <td>
                                     <asp:DropDownList ID="drp_catTwo" runat="server" AutoPostBack="true" DataValueField="Cat2ID" DataTextField="C2Name" CssClass="form-control select2" OnSelectedIndexChanged="drp_catTwo_SelectedIndexChanged"></asp:DropDownList>
                                 </td>
                                 <td>
                                     <asp:DropDownList ID="drp_catThree" runat="server" AutoPostBack="true" DataValueField="Cat3ID" DataTextField="C3Name" CssClass="form-control select2" OnSelectedIndexChanged="drp_catThree_SelectedIndexChanged"></asp:DropDownList>
                                 </td>
                                 <td>
                                     <asp:DropDownList ID="drp_catFour" runat="server" AutoPostBack="true" DataValueField="Cat4ID" DataTextField="C4Name" CssClass="form-control select2" OnSelectedIndexChanged="drp_catFour_SelectedIndexChanged"></asp:DropDownList>
                                 </td>
                                 <td>
                                     <asp:DropDownList ID="drp_catFive" runat="server" AutoPostBack="true" DataValueField="Cat5ID" DataTextField="C5Name" CssClass="form-control select2"></asp:DropDownList>
                                 </td>
                                 <td><asp:TextBox ID="drpStyles" runat="server" CssClass="form-control"></asp:TextBox></td>
                             </tr>
                             <tr id="searchLabel" runat="server" visible="false">
                                 <td id="drp_itemCategorylbl"></td>
                                 <td id="drp_catTwolbl"></td>
                                 <td id="drp_catThreelbl"></td>
                                 <td id="drp_catFourlbl"></td>
                                 <td id="drp_catFivelbl"></td>
                                 <td id="drpStyleDisplay"></td>
                             </tr>
                         </table>
                                
                        
                        <div class="box-body table-responsive">
                            <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                                <asp:Button ID="btnRefresh"  class="btn btn-info pull-right btn-round" runat="server" Text="Refresh" OnClick="btnRefresh_Click"/>
                                
                                <asp:Button ID="btnFilter"  class="btn btn-warning pull-right btn-round" runat="server" Text="Filter" OnClick="btnFilter_Click"/>
                                <asp:Button ID="btnShowAll"  class="btn btn-primary pull-right btn-round" runat="server" Text="Show All" OnClick="btnShowAll_Click"/>
                            </div>
                        </div> 
                    </div>
                                    </ContentTemplate></asp:UpdatePanel>
                        </asp:Panel>
                        <div class="row" runat="server" visible="false" id="filterHideShow">
                            <div class="col-md-6 col-xs-12 col-lg-6">
                            <table class="table table-bordered table-striped">
                                <tr>
                                    <th>Select</th>
                                    <th>Data Field Value</th>
                                    <th>Search Text</th>
                                </tr>
                                <tr>
                                    <td><asp:CheckBox ID="controlNameTitle" runat="server" value="Title"/></td>
                                    <td>Title</td>
                                    <td><asp:TextBox ID="titleSearch" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                      
                                <asp:Repeater ID="rpt_DataField" runat="server" OnItemDataBound="rpt_DataField_ItemDataBound">
                                   <ItemTemplate>
                                       <tr id="datafieldHideShow" runat="server">
                                           <td>
                                               <asp:Label ID="IsAssignedDataField" runat="server" Visible="false" Text='<%# Eval("IsAssigned").ToString() %>'></asp:Label>
                                               <asp:CheckBox ID="controlName" runat="server" value='<%# Eval("ColumnNo").ToString() %>'/></td>
                                           <td><%# Eval("SettingName").ToString() %></td>
                                           <td><asp:TextBox ID="controlSearch" runat="server" CssClass="form-control"></asp:TextBox></td>
                                       </tr>
                                   </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            </div>
                            <div class="col-md-6 col-xs-12 col-lg-6">
                                <table class="table table-bordered table-striped">
                                    <tr>
                                        <th>Select</th>
                                        <th>DropDown Value</th>
                                        <th>DropDown Search</th>
                                    </tr>
                             
                                     <asp:Repeater ID="rpt_dropdown" runat="server" OnItemDataBound="rpt_dropdown_ItemDataBound">
                                   <ItemTemplate>
                                       <tr id="drpHideShow" runat="server">
                                           <td>
                                               <asp:Label ID="IsAssigneddrp" runat="server" Visible="false" Text='<%# Eval("IsAssigned").ToString() %>'></asp:Label>
                                               <asp:CheckBox ID="drpName" runat="server" value='<%# Eval("checkValue").ToString() %>'/></td>
                                           <td><%# Eval("SettingName").ToString() %></td>
                                           <td>
                                               <asp:DropDownList ID="drpCols" runat="server" CssClass="form-control select2" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                                           </td>
                                       </tr>
                                        </ItemTemplate>
                                </asp:Repeater>
                                </table>
                            </div>
                        <div class="box-body table-responsive ">
                            <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                                <asp:Button ID="searchbyFilter"  class="btn btn-primary pull-right btn-round" runat="server" Text="Search" OnClick="searchbyFilter_Click"/>
                               
                            </div>
                        </div>
                        </div>
                           
                      

					<div runat="server" id="divMainStockList" visible="false">
						<div class="row">
							<div class="col-xs-12">
								<div class="box">
                                    <div>
                               <asp:label runat="server" ID="successfailure" Text="" Visible="false" style="color:crimson;text-align:center;"></asp:label>
                           </div>
									<div class="box-body table-responsive">
										<table id="example11" class="table table-bordered table-striped ">
											<thead>
												<tr>
													<th>Action</th>
													<%--<th>Sell</th>--%>
													<th>Style Code</th>
													<th>Title</th>
													<th>MRP</th>
													<th>Item Category</th>
													<th>Available Size</th>
													<%--      <th>Listed Status</th>                        --%>
												</tr>
											</thead>
											<tbody>
												<asp:Repeater ID="rptViewInventory" runat="server" OnItemDataBound="rptViewInventory_ItemDataBound">
													<ItemTemplate>
														<tr>
															<td>
																<asp:LinkButton ID="btnList" class="btn btn-primary" OnClick="btnList_Click"
																	runat="server">List</asp:LinkButton>
																<asp:LinkButton ID="btnSell" class="btn btn-danger" OnClick="btnSell_Click"
																	runat="server">Sell</asp:LinkButton>
                                                                
															</td>

															<td>
																<asp:Label ID="rptlblStyleId" runat="server" Text='<%# Eval("StyleID")%>' Visible="false"></asp:Label><asp:Label ID="rptlblStyleCode" runat="server" Text='<%# Eval("StyleCode")%>'></asp:Label>
															</td>
															<td>
																<asp:Label ID="rptlblTitle" runat="server" Text='<%# Eval("Title")%>'></asp:Label></td>
															<td>
																<asp:Label ID="rptlblMRP" runat="server" Text='<%# Eval("mrp")%>'></asp:Label></td>
															<td>
																<asp:Label ID="rptlblItemCat" runat="server" Text='<%# Eval("ItemCategory")%>'></asp:Label></td>
															<td>
																<asp:Label ID="rptlblSizeDet" runat="server" Text=""></asp:Label></td>
															<%--<td><asp:Label ID="lblListDet" runat="server" Text=""></asp:Label></td>--%>
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
					</div>

					<div runat="server" id="divDetails" visible="false">
						<div class="row">
							<div class="col-xs-12">
								<div class="box">
									<div class="box-body table-responsive">
										<table id="example112" class="table table-bordered table-striped ">
											<thead>
												<tr>

													<th>Style Code</th>
													<th>Title</th>
													<th>MRP</th>
													<th>Item Category</th>
													<th>Available Size</th>
                                                    <th>View</th>
												</tr>
											</thead>
											<tbody>

												<tr>

													<td>
														<asp:Label ID="lblStyleId" runat="server" Text='<%# Eval("StyleID")%>' Visible="false"></asp:Label><asp:Label ID="lblStyleCode" runat="server" Text='<%# Eval("StyleCode")%>'></asp:Label>
													</td>
													<td>
														<asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title")%>'></asp:Label></td>
													<td>
														<asp:Label ID="lblMRP" runat="server" Text='<%# Eval("mrp")%>'></asp:Label></td>
													<td>
														<asp:Label ID="lblItemCat" runat="server" Text='<%# Eval("ItemCategory")%>'></asp:Label></td>
													<td>
														<asp:Label ID="lblSizeDet" runat="server" Text=""></asp:Label></td>
                                                    <td><asp:LinkButton ID="btnItemStyleDetails" runat="server" OnClick="btnItemStyleDetails_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-eye"></i> View</asp:LinkButton></td>

												</tr>

											</tbody>
											<tfoot>
											</tfoot>
										</table>
									</div>
								</div>
							</div>
						</div>


					</div>

					<div runat="server" id="divItemList" visible="false">
						<div class="row">
							<div class="col-xs-12">
								<div class="box">
									<div class="box-header with-border">
										<h3 class="box-title">List Items</h3>

									</div>
									<!-- /.box-header -->
									<div class="box-body">
										<div class="col-md-4">
											<div class="form-group">

												<label>Virtual Location</label>
												<asp:DropDownList ID="cmbListLoc" runat="server" DataTextField="Location" DataValueField="locationID" AutoPostBack="true" OnSelectedIndexChanged="cmbListLoc_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
											</div>
										</div>
										<div class="col-md-8" visible="false" runat="server">
											<div class="form-group">

												<label>List Id</label>
												<asp:TextBox ID="txtlistidgivenbyvloc" runat="server" CssClass="form-control" Text=""></asp:TextBox></td>

											</div>
										</div>
										<div class="col-md-12">
											<table class="table table-bordered table-striped">
												<tr>
													<th>Select</th>
													<th>SKU</th>
                                                    <th>List Id</th>
													<th>List Price</th>
												</tr>
												<asp:Repeater ID="rptListItems" runat="server" OnItemDataBound="rptListItems_ItemDataBound">
													<ItemTemplate>
														<tr>
															<td>
																<asp:Label ID="rptlblstockUpId" runat="server" Visible="false" Text=''></asp:Label>
                                                                <asp:Label ID="rptSizeId" runat="server" Visible="false" Text='<%# Eval("SizeID").ToString() %>'></asp:Label>
                                                                <asp:Label ID="rptStyleId" runat="server" Visible="false" Text='<%# Eval("StyleID").ToString() %>'></asp:Label>
																<asp:Label ID="rptlblListId" runat="server" Visible="false" Text='<%# Eval("listid").ToString() %>'></asp:Label>
                                                                <asp:Label ID="fullBarcode" runat="server" Visible="false" Text='<%# Eval("fullBarcode").ToString() %>'></asp:Label>
																<asp:CheckBox ID="chkListed" runat="server" Checked='<%#Convert.ToBoolean(Eval("chkItem").ToString()) %>' /></td>
															<td><%# Eval("fullBarcode").ToString() %></td>
															<td><asp:TextBox ID="listidgivenbyvloc" runat="server" CssClass="form-control" Text='<%# Eval("listidgivenbyvloc").ToString() %>'></asp:TextBox></td>
                                                            <td>
																<asp:TextBox ID="rpttxtListPrice" runat="server" CssClass="form-control" Text='<%# Eval("listprice").ToString() %>'></asp:TextBox></td>
														</tr>
													</ItemTemplate>
												</asp:Repeater>

											</table>
										</div>
									</div>
									<div class="box-footer btn-toolbar " style="margin-left: 0px;">

										<asp:Button ID="btnSaveListDet" class="btn btn-primary pull-right btn-round" runat="server" Text="List" OnClick="btnSaveListDet_Click" />

									</div>
								</div>
							</div>
						</div>


					</div>

					<div runat="server" id="divSales" visible="false">
						<div class="row">
							<div class="col-xs-12">
								<div class="box">
									<div class="box-header with-border">
										<h3 class="box-title">Sales</h3>


									</div>
									<!-- /.box-header -->
									<div class="box-body">
                                        <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                    </div>
										<div class="col-md-6">
											<table class="table table-bordered table-striped">
												<tr>
													<th>Virtual location</th>
													<td>
														<%--<asp:Label ID="lblStyleID" runat="server" Text="" visible="false"></asp:Label>--%>
														<asp:Label ID="lblGSTPercent" runat="server" Text="" Visible="false"></asp:Label>
														<asp:DropDownList ID="virtualLocation" runat="server" DataTextField="Location" DataValueField="locationID" AutoPostBack="true" OnSelectedIndexChanged="virtualLocation_SelectedIndexChanged" CssClass="form-control select2"></asp:DropDownList></td>
												</tr>
												<tr>
													<th>SalesID</th>
													<td>
														<asp:TextBox ID="salesId" runat="server" CssClass="form-control"></asp:TextBox></td>
												</tr>

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

                                                <tr>
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
                                                    </td>
                                                </tr>
											</table>
										</div>


										<div class="col-md-6 table-responsive">
                                            
											<table class="table table-bordered table-striped dtSearch">
												<thead><tr>
													<th>Select</th>
													<th>Barcode</th>
                                                    <th>Status</th>
													<th>Location</th>
													<th>SP</th>
                                                    <th>View</th>
												</tr></thead>
                                                <tbody>
												<asp:Repeater ID="rptSales" runat="server">
													<ItemTemplate>
														<tr id="salesTR" runat="server">
															<td>
                                                                <asp:Label ID="lblBarcodeNoPop" runat="server" Visible="false" Text='<%# Eval("fullBarcode").ToString() %>'></asp:Label>
																<asp:Label ID="stockUpId" runat="server" Visible="false" Text='<%# Eval("StockUpID").ToString() %>'></asp:Label>
																<asp:CheckBox ID="sales" runat="server" /></td>
															<td><%# Eval("fullBarcode").ToString() %></td>
                                                            <td><%# Eval("Status").ToString() %></td>
															<td><%# Eval("RackBarcode").ToString() %></td>
															<td>
																<asp:TextBox ID="sp" Width="100px" runat="server" CssClass="form-control" Text='<%# Eval("listprice").ToString() %>'></asp:TextBox></td>
														    <td><asp:LinkButton ID="showBarcodeStatus" runat="server" CssClass="btn btn-dropbox btn-sm" OnClick="showBarcodeStatus_Click"><i class="fa fa-eye"></i></asp:LinkButton></td>
                                                        </tr>
													</ItemTemplate>
												</asp:Repeater>

                                     
                                                    </tbody>
											</table>
										</div>
									</div>
									<div class="box-footer btn-toolbar " style="margin-left: 0px;">

										<asp:Button ID="saveSales" class="btn btn-primary pull-right btn-round" runat="server" Text="Generate Invoice" OnClick="saveSales_Click" />

									</div>
								</div>
							</div>
						</div>


					</div>

					<div runat="server" id="divBack" visible="false">
						<div class="row">
							<div class="col-xs-12">
								<div class="box">

									<div class="box-footer btn-toolbar " style="margin-left: 0px;">

										<asp:Button ID="btnBackToIventory" class="btn btn-warning pull-right " runat="server" Text="Back to Inventory" OnClick="btnBackToIventory_Click" />

									</div>
								</div>
							</div>
						</div>


					</div>
                    <asp:Panel ID="PanelDets" runat="server" Style="display: none;">
                <!--height:50%!important;width:50%!important;-->

                <asp:Button ID="Button8" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender7"
                    runat="server"
                    TargetControlID="Button8"
                    PopupControlID="PanelDets"
                    BackgroundCssClass="modalBackground" BehaviorID="mpe7">
                </cc1:ModalPopupExtender>

                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                           
                            <h4 class="modal-title">
                           <asp:Label ID="lblItemNamedets" runat="server" Text=''></asp:Label>
                                <asp:Label ID="lblDetsStyleID" runat="server" Text=''></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body table-responsive" style="overflow-y:scroll;height:300px;">
                          
                        <table class="table table-bordered table-striped">
                            <tr>
                                <th>Item Category </th>
                                <th><asp:Label ID="DetItemCatname2" runat="server" Text=''></asp:Label> </th>
                                <th><asp:Label ID="DetItemCatname3" runat="server" Text=''></asp:Label> </th>
                                <th><asp:Label ID="DetItemCatname4" runat="server" Text=''></asp:Label> </th>
                                <th><asp:Label ID="DetItemCatname5" runat="server" Text=''></asp:Label> </th>
                                
                            </tr>
                            <tr>
                                <td><asp:Label ID="DetItemCatVal1" runat="server" Text=''></asp:Label></td>
                                <td><asp:Label ID="DetItemCatVal2" runat="server" Text=''></asp:Label></td>
                                <td><asp:Label ID="DetItemCatVal3" runat="server" Text=''></asp:Label></td>
                                <td><asp:Label ID="DetItemCatVal4" runat="server" Text=''></asp:Label></td>
                                <td><asp:Label ID="DetItemCatVal5" runat="server" Text=''></asp:Label></td>
                            </tr>
                             
                        </table>
                        <table class="table table-bordered table-striped" >
                                                        <tr>
                                    
                                                            <th>Data Field Value</th>
                                                            <th>Search Text</th>
                                                        </tr>
                                                        
                                                         <tr>
                                    
                                                            <td>MRP</td>
                                                            <td><asp:Label ID="detMrp" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <asp:Repeater ID="rptDataFieldDets" runat="server" OnItemDataBound="rptDataFieldDets_ItemDataBound">
                                                           <ItemTemplate>
                                                               <tr id="datafieldHideShow" runat="server">
                                                                   <td><asp:Label ID="IsAssignedDataField" runat="server" Visible="false" Text='<%# Eval("IsAssigned").ToString() %>'></asp:Label><%# Eval("SettingName").ToString() %></td>
                                                                   <td><asp:Label ID="controlSearch" runat="server" Text='<%# Eval("colVal").ToString() %>'></asp:Label></td>
                                                               </tr>
                                                           </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                        
                         <table class="table table-bordered table-striped">
                                    <tr>
                                        
                                        <th>DropDown Value</th>
                                        <th>DropDown Search</th>
                                    </tr>
                                     <asp:Repeater ID="rptDrop" runat="server" OnItemDataBound="rptDrop_ItemDataBound">
                                   <ItemTemplate>
                                       <tr id="drpHideShow" runat="server">
                                           
                                           <td>
                                               <asp:CheckBox ID="drpName" runat="server" value='<%# Eval("checkValue").ToString() %>' Visible="false"/>
                                               <%# Eval("SettingName").ToString() %></td>
                                           <td><asp:Label ID="IsAssigneddrp" runat="server" Visible="false" Text='<%# Eval("IsAssigned").ToString() %>'></asp:Label>
                                               <asp:Label ID="colName" runat="server" Text=''></asp:Label>
                                           </td>
                                       </tr>
                                        </ItemTemplate>
                                </asp:Repeater>
                                </table>

                             <table class="table table-bordered table-striped">
                                 <tr>
                                     <th>Image 1</th>
                                     <th>Image 2</th>
                                 </tr>
                                 <tr>
                                     <td>
                                         <asp:image ID="image1Display" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/>
                                     </td>
                                     <td>
                                         <asp:image ID="image2Display" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/>
                                     </td>
                                 </tr>
                                  <tr>
                                     <th>Image 3</th>
                                     <th>Image 4</th>
                                 </tr>
                                  <tr>
                                     <td>
                                         <asp:image ID="image3Display" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/>
                                     </td>
                                     <td>
                                         <asp:image ID="image4Display" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/>
                                     </td>
                                 </tr>
                                  <tr>
                                     <th>Image 5</th>
                                     <th>Image 6</th>
                                 </tr>
                                  <tr>
                                     <td>
                                         <asp:image ID="image5Display" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/>
                                     </td>
                                     <td>
                                         <asp:image ID="image6Display" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/>
                                     </td>
                                 </tr>
                                 </table>
                       

              </div>
              <div class="modal-footer btn-toolbar">
                  <asp:Button ID="Button9" CssClass="btn btn-danger pull-right" runat="server" Text="Close" OnClientClick="return HideModalPopup7()" />
                  
                  
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
                <!-- /.modal-dialog -->
       


            </asp:Panel>


                    <asp:Panel ID="PanelSinglePopUp" runat="server" Style="display: none;">
                <!--height:50%!important;width:50%!important;-->

                <asp:Button ID="Button99" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender299"
                    runat="server"
                    TargetControlID="Button99"
                    PopupControlID="PanelSinglePopUp"
                    BackgroundCssClass="modalBackground" BehaviorID="mpe99">
                </cc1:ModalPopupExtender>

                <div class="modal-dialog">
                    <div class="modal-content cap-mod">
                        <div class="modal-header">
                           
                            <h4 class="modal-title">BARCODE : <asp:Label runat="server" ID="lblBarcodeMaster"></asp:Label>
                               
                            </h4>
                        </div>
                        <div class="modal-body table-responsive">
                   <div class="col-md-12">
        <table class="table table-bordered table-stripped">
                                <thead>
                                <tr>
                                    
                                    <th>User</th>
                                    <th>Style Action</th>
                                    <th>DateTime</th>
                                    
                                   
                                </tr>
                                    </thead>
                                <tbody>
                                 <asp:Repeater ID="rpt_StyleMaster" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                           
                                           <td><%# Eval("User").ToString() %></td>
                                           <td><%# Eval("Details").ToString() %></td>
                                           <td><%# Eval("Dets").ToString() %></td>
                                           
                                       </tr>
                                       </ItemTemplate>
                                </asp:Repeater>
                                    </tbody>
                            </table>

                       <table class="table table-bordered table-stripped dtSearchDesc">
                                <thead>
                                <tr>
                                    <th>User</th>
                                    <th>Barcode Status</th>
                                    <th>DateTime</th>
                                   
                                </tr>
                                    </thead>
                                <tbody>
                                 <asp:Repeater ID="rtp_ListMaster" runat="server">
                                   <ItemTemplate>
                                       <tr>
                                           <td><%# Eval("User").ToString() %></td>
                                           <td><%# Eval("Status").ToString() %></td>
                                           <td><%# Convert.ToDateTime(Eval("DateTime")).ToString("dd MMM yyyy HH:mm:ss tt") %></td>
                                           
                                       </tr>
                                       </ItemTemplate>
                                </asp:Repeater>
                                    </tbody>
                            </table>
					</div>
              </div>
              <div class="modal-footer btn-toolbar">
                  <asp:Button ID="Button333" CssClass="btn btn-danger pull-right" runat="server" Text="Close" OnClientClick="return HideModalPopup99()" />
                  
        
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
                <!-- /.modal-dialog -->
       


            </asp:Panel>
				</ContentTemplate>
			</asp:UpdatePanel>
            
		</section>
	</div>
</asp:Content>
