<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="addStyle.aspx.cs" Inherits="addStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="content-wrapper">
		<section class="content-header">
			<h1>Add Item Style 
        <!--<small>it all starts here</small>-->
			</h1>
			<ol class="breadcrumb">
				<li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
				<li><a href="ItemStyleSearchAdd.aspx">Item Style Search</a></li>
				<li class="active">Add Item Style</li>
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
									<h3 class="box-title">Add Item Style</h3>

								</div>
								<!-- /.box-header -->
								<div class="box-body">
									<div class="row">
                                           <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                    </div>
										<asp:Label ID="StyleID" runat="server" Text="" Visible="false"></asp:Label>
										<table class="table table-bordered" runat="server" id="images">
											<tr>
												<th>Vertical
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
											</tr>

										</table>

									</div>

									<div class="box box-primary">
										<div class="box-header with-border">
											<h3 class="box-title">Default Data</h3>

										</div>
										<!-- /.box-header -->
										<div class="box-body">
											<div class="col-md-12 col-xs-12 col-lg-12">
												<div class="row table-responsive">
													<table class="table table-bordered table-striped">
														<tr>
															<th>Title</th>
															<td>
																<asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Text=""></asp:TextBox></td>
															<th>MRP</th>
															<td>
																<asp:TextBox ID="mrp" runat="server" CssClass="form-control" Text=""></asp:TextBox></td>
														</tr>

													</table>

													<table class="table table-bordered table-striped">
														<tr>
															<th width="35%">Image1</th>
															<th width="35%">Image2</th>
															<th width="35%">Image3</th>
														</tr>
														<tr>
															<td>
																<asp:FileUpload ID="FileUpload1" runat="server" /></td>
															<td>
																<asp:FileUpload ID="FileUpload2" runat="server" /></td>
															<td>
																<asp:FileUpload ID="FileUpload3" runat="server" /></td>
														</tr>
														<tr>
															<td>
																<asp:Image ID="image1Display" runat="server" ImageUrl="..." Visible="false" Height="100px" Width="100px" />
                                                                </br>
                                                                <asp:CheckBox ID="image1DisplayDel" runat="server" visible="false"/> Remove Image
															</td>
															<td>
																<asp:Image ID="image2Display" runat="server" ImageUrl="..." Visible="false" Height="100px" Width="100px" />
                                                                </br>
                                                                <asp:CheckBox ID="image2DisplayDel" runat="server" visible="false"/> Remove Image
															</td>
															<td>
																<asp:Image ID="image3Display" runat="server" ImageUrl="..." Visible="false" Height="100px" Width="100px" />
                                                                </br>
                                                                <asp:CheckBox ID="image3DisplayDel" runat="server" visible="false"/> Remove Image
															</td>
														</tr>
														<tr>
															<th>Image4</th>
															<th>Image5</th>
															<th>Image6</th>
														</tr>
														<tr>
															<td>
																<asp:FileUpload ID="FileUpload4" runat="server" /></td>
															<td>
																<asp:FileUpload ID="FileUpload5" runat="server" /></td>
															<td>
																<asp:FileUpload ID="FileUpload6" runat="server" /></td>
														</tr>
														<tr>
															<td>
																<asp:Image ID="image4Display" runat="server" ImageUrl="..." Visible="false" Height="100px" Width="100px" />
                                                                </br>
                                                                <asp:CheckBox ID="image4DisplayDel" runat="server" visible="false"/> Remove Image
															</td>
															<td>
																<asp:Image ID="image5Display" runat="server" ImageUrl="..." Visible="false" Height="100px" Width="100px" />
                                                                </br>
                                                                <asp:CheckBox ID="image5DisplayDel" runat="server" visible="false"/> Remove Image
															</td>
															<td>
																<asp:Image ID="image6Display" runat="server" ImageUrl="..." Visible="false" Height="100px" Width="100px" />
                                                                </br>
                                                                <asp:CheckBox ID="image6DisplayDel" runat="server" visible="false"/> Remove Image
															</td>
														</tr>
													</table>
												</div>
											</div>
										</div>

										<div class="box box-primary">
											<div class="box-header with-border">
												<h3 class="box-title">Dynamic Data</h3>

											</div>
											<!-- /.box-header -->
											<div class="box-body">
												<div class="row" runat="server">
													<div class="col-md-6 col-xs-12 col-lg-6">
														<table class="table table-bordered table-striped">
															<tr>
																<th><asp:CheckBox ID="selectDeselect" AutoPostBack="true" runat="server" OnCheckedChanged="selectDeselect_CheckedChanged"/> Check All</th>
																<th>Data Field Value</th>
																<th>Search Text</th>
															</tr>

                                                            
															<asp:Repeater ID="rpt_DataField" runat="server" OnItemDataBound="rpt_DataField_ItemDataBound">
																<ItemTemplate>
																	<tr id="datafieldHideShow" runat="server">
																		<td>
																			<asp:Label ID="IsAssignedDataField" runat="server" Visible="false" Text='<%# Eval("IsAssigned").ToString() %>'></asp:Label>
                                                                           
                                                                            <asp:Label ID="lblcolno" runat="server" Visible="false" Text='<%# Eval("StyleCSID").ToString() %>'></asp:Label>
																			<asp:CheckBox ID="controlName" runat="server" value='<%# Eval("ColumnNo").ToString() %>' />

																		</td>
																		<td runat="server" id="settingFieldName"><%# Eval("SettingName").ToString() %></td>
																		<td>
																			<asp:TextBox ID="controlSearch" runat="server" CssClass="form-control"></asp:TextBox></td>
																	</tr>
																</ItemTemplate>
															</asp:Repeater>
														</table>
													</div>
													<div class="col-md-6 col-xs-12 col-lg-6">
														<table class="table table-bordered table-striped">
															<tr>
																<th><asp:CheckBox ID="drpSelect" AutoPostBack="true" runat="server" OnCheckedChanged="drpSelect_CheckedChanged"/> Check All</th>
																<th>DropDown Value</th>
																<th>DropDown Search</th>
															</tr>
															<asp:Repeater ID="rpt_dropdown" runat="server" OnItemDataBound="rpt_dropdown_ItemDataBound">
																<ItemTemplate>
																	<tr id="drpHideShow" runat="server">
																		<td>
																			<asp:Label ID="IsAssigneddrp" runat="server" Visible="false" Text='<%# Eval("IsAssigned").ToString() %>'></asp:Label>
                                                                            <asp:Label ID="defaultSelection" runat="server" Visible="false" Text='<%# Eval("defaultSelection").ToString() %>'></asp:Label>
                                                                             <asp:Label ID="lblcolno" runat="server" Visible="false" Text='<%# Eval("CTSettingID").ToString() %>'></asp:Label>
																			<asp:CheckBox ID="drpName" runat="server" value='<%# Eval("checkValue").ToString() %>' /></td>
																		<td  runat="server" id="settingDrpName"><%# Eval("SettingName").ToString() %></td>
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
                                                            <asp:Button ID="cancelStyle" runat="server" Text="Cancel" CssClass="btn btn-sm btn-danger pull-right" OnClick="cancelStyle_Click" OnClientClick="return confirm('Do you Want to Cancel ?');" />
															<asp:Button ID="saveStyle" runat="server" Text="Save Style" CssClass="btn btn-sm btn-success pull-right" OnClick="saveStyle_Click" OnClientClick="return confirm('Do you Want to Save Style ?');" />
                                                            


														</div>
													</div>
												</div>
											</div>
										</div>
									</div>

								</div>
							</div>
						</div>
					</div>
					<!-- /.row -->
					<%--<div id="dialog" style="display: none">
</div>--%>

				</section>
			</ContentTemplate>

			<Triggers>
				<asp:PostBackTrigger ControlID="saveStyle" />
			</Triggers>
		</asp:UpdatePanel>
	</div>
	<%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.24/themes/start/jquery-ui.css" />
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.24/jquery-ui.min.js"></script>s
		<script type="text/javascript">
    $(function () {
        $("#dialog").dialog({
            autoOpen: false,
            modal: true,
            height: 600,
            width: 600,
            title: "Zoomed Image"
        });
        $("[id*=images] img").click(function () {
            $('#dialog').html('');
            $('#dialog').append($(this).clone());
            $('#dialog').dialog('open');
        });
    });
</script>--%>
</asp:Content>

