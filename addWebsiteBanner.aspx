<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="addWebsiteBanner.aspx.cs" Inherits="addWebsiteBanner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
		<section class="content-header">
			<h1>Add Website Banner</h1>
			<ol class="breadcrumb">
				<li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
				<li><a href="websiteBanners.aspx">Website Banners</a></li>
				<li class="active">Add Website Banner</li>
			</ol>
		</section>
        <section class="content">
            	<!-- Default box -->
					<div class="box">
						<div class="box-body">
							<!-- BOx -->
							<div class="box box-primary">
								<div class="box-header with-border">
									<h3 class="box-title">Add Website</h3>

								</div>
								<!-- /.box-header -->
								<div class="box-body table-responsive">
									<div class="row">
                                        <asp:Label ID="Label1" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                                           <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                    </div>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>

                                           
                            <asp:Label ID="bannerId" runat="server" Text="" Visible="false"></asp:Label>
                                      
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Banner Name</label>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="bannerName"
        ValidationGroup="save" runat="server" ErrorMessage="Banner Name is required." forecolor="Red" /><br />
                                    <asp:TextBox ID="bannerName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            
                               <div class="col-md-6">
                                <div class="form-group">
                                    <label>Banner Type</label>
                                    <asp:DropDownList ID="bannerType" runat="server" CssClass="form-control select2">
                                        <asp:ListItem Value="1">Sliding Banner (300KB,H:500px,W:1920px)</asp:ListItem>
                                        <asp:ListItem Value="2">Random Banner (25KB,H:200px,W:326px)</asp:ListItem>
                                        <asp:ListItem Value="3">Deals of The Day (25KB,H:360px,W:360px)</asp:ListItem>
                                        <asp:ListItem Value="4">Hot Deals (100KB,H:756px,W:920px)</asp:ListItem>
                                        <asp:ListItem Value="5">Banner 5 (25KB,H:258px,W:316px)</asp:ListItem>
                                        <asp:ListItem Value="6">Banner 6 (25KB,H:258px,W:316px)</asp:ListItem>
                                        <asp:ListItem Value="7">Menu Image (25KB,H:200px,W:326px)</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Banner Image</label>
                                    <asp:RegularExpressionValidator ID="revImage" ValidationGroup="save" ControlToValidate="FileUpload1" forecolor="Red" ValidationExpression="(.*?)\.(jpg|jpeg|png|gif|JPG|JPEG|PNG|GIF)$" Text="Only .jpg,.png,.jpeg,.gif Files are allowed" runat="server" />
                                    <asp:FileUpload ID="FileUpload1" runat="server" /><br/>
                                    <asp:Image ID="image1Display" runat="server" ImageUrl="..." Visible="false" Height="100px" Width="100px" />
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-12">
                                        <asp:Button ID="dropdownAdd" runat="server" Text="Add New" class="btn bg-purple btn-round margin btn-sm" OnClick="dropdownAdd_Click" />
                                    </div>
                                <table class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>DropDown Values</th>
                                            <th>Values</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rpt_dropdown" runat="server" OnItemDataBound="rpt_dropdown_ItemDataBound">
                                                <ItemTemplate>
                                                <tr>
                                                    <td> 
                                                        <asp:Label runat="server" ID="TableName" Text='<%# Eval("TableName").ToString() %>' Visible="false"></asp:Label>
                                                        <asp:DropDownList ID="drp_dropdown" CssClass="form-control select2" runat="server" DataTextField="SettingName" DataValueField="CTSettingID" AutoPostBack="true" OnSelectedIndexChanged="drp_dropdown_SelectedIndexChanged"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="drp_dropValues" CssClass="form-control select2" runat="server" DataTextField="valueName" DataValueField="valueId"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                       </ItemTemplate></asp:Repeater>
                                    </tbody>
                                </table>
                            </div>

                            <div class="col-md-12">
                                <table class="table table-bordered table-striped">
															<tr>
																<th>Select</th>
																<th>Gender</th>
																
															</tr>
															<asp:Repeater ID="rpt_gender" runat="server">
																<ItemTemplate>
																	<tr>
																		<td>
																			<asp:Label ID="bgId" runat="server" Visible="false" Text='<%# Eval("bgId").ToString() %>'></asp:Label>                                                                            
                                                                            <asp:CheckBox ID="genCheck" runat="server" value='<%# Eval("genderID").ToString() %>' Checked='<%# Convert.ToBoolean(Eval("checkValue").ToString()) %>' /></td>
																		<td><%# Eval("genderName").ToString() %></td>
																		
																	</tr>
																</ItemTemplate>
															</asp:Repeater>
														</table>

                    <table class="table table-bordered table-striped">
															<tr>
																<th>Select</th>
																<th>Vertical</th>
																<th>Sub Category</th>
															</tr>
															<asp:Repeater ID="rpt_vertical" runat="server" OnItemDataBound="rpt_vertical_ItemDataBound">
																<ItemTemplate>
																	<tr>
																		<td>
																			<asp:Label ID="bvId" runat="server" Visible="false" Text='<%# Eval("bvId").ToString() %>'></asp:Label>                                                                            
                                                                            <asp:RadioButton ID="verCheck" runat="server" value='<%# Eval("ItemCategoryID").ToString() %>' Checked='<%# Convert.ToBoolean(Eval("checkValue").ToString()) %>' AutoPostBack="true" OnCheckedChanged="verCheck_CheckedChanged"/></td>
																		<td><%# Eval("ItemCategory").ToString() %></td>
																		<td>
                                                                            <table class="table table-bordered table-striped">
                                                                                <tr>
																                    <th>Select</th>
																                    <th>Sub Category</th>																                   
															                    </tr>
                                                                                <asp:Repeater ID="rpt_category" runat="server">
																                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
																			                    <asp:Label ID="bcId" runat="server" Visible="false" Text='<%# Eval("bcId").ToString() %>'></asp:Label>                                                                            
                                                                                                <asp:CheckBox ID="catCheck" runat="server" value='<%# Eval("categoryID").ToString() %>' Checked='<%# Convert.ToBoolean(Eval("checkValue").ToString()) %>' /></td>
																		                    <td><%# Eval("categoryName").ToString() %></td>
                                                                                        </tr>
																                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </table>
																		</td>

																	</tr>
																</ItemTemplate>
															</asp:Repeater>
														</table>
                            </div>
                                        <div class="col-md-12">
                                            <div class="box-body table-responsive ">
														<div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                            <asp:Button ID="cancel" runat="server" Text="Cancel" CssClass="btn btn-sm btn-danger pull-right" OnClick="cancel_Click" OnClientClick="return confirm('Do you Want to Cancel ?');" />
															<asp:Button ID="save" runat="server" Text="Save" CssClass="btn btn-sm btn-success pull-right" OnClick="save_Click" ValidationGroup="save" OnClientClick="return confirm('Do you Want to Save Website Banner Settings ?');" />
                                                            


														</div>
													</div>
                                        </div>
 </ContentTemplate>
            <Triggers>
				<asp:PostBackTrigger ControlID="save" />
			</Triggers>
                                        </asp:UpdatePanel>

                                        </div></div></div></div></div>
        </section>
    </div>
</asp:Content>

