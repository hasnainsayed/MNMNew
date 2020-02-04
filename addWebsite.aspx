<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="addWebsite.aspx.cs" Inherits="addWebsite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
		<section class="content-header">
			<h1>Add Website</h1>
			<ol class="breadcrumb">
				<li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
				<li><a href="sellOnWebsite.aspx">Sell On Website</a></li>
				<li class="active">Add Website</li>
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
                                           <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                    </div>
                            <asp:Label ID="sellId" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="Label1" runat="server" Text="" Visible="false"></asp:Label>  
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Display Title</label>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="displayTitle"
        ValidationGroup="save" runat="server" ErrorMessage="Display Title is required." forecolor="Red" /><br />
                                    <asp:TextBox ID="displayTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Display Status</label>
                                    <asp:RadioButtonList ID="displayStatus" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                                                       <div class="col-md-6">
                                <div class="form-group">
                                    <label>Priority</label>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="priorities"
        ValidationGroup="save" runat="server" ErrorMessage="Priority is required." forecolor="Red" /><br />
                                    <asp:TextBox ID="priorities" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                                        <div class="col-md-6">
                                <div class="form-group">
                                    <label>Menu Banner</label>
                                    <asp:DropDownList ID="menuBannerId" runat="server" CssClass="form-control select2" DataTextField="bannerName" DataValueField="bannerId"></asp:DropDownList>
                                </div>
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
																			<asp:Label ID="wgId" runat="server" Visible="false" Text='<%# Eval("wgId").ToString() %>'></asp:Label>                                                                            
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
																			<asp:Label ID="wvId" runat="server" Visible="false" Text='<%# Eval("wvId").ToString() %>'></asp:Label>                                                                            
                                                                            <asp:CheckBox ID="verCheck" runat="server" value='<%# Eval("ItemCategoryID").ToString() %>' Checked='<%# Convert.ToBoolean(Eval("checkValue").ToString()) %>' AutoPostBack="true" OnCheckedChanged="verCheck_CheckedChanged"/></td>
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
																			                    <asp:Label ID="wcId" runat="server" Visible="false" Text='<%# Eval("wcId").ToString() %>'></asp:Label>                                                                            
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
															<asp:Button ID="save" runat="server" Text="Save" CssClass="btn btn-sm btn-success pull-right" OnClick="save_Click" ValidationGroup="save" OnClientClick="return confirm('Do you Want to Save Website Settings ?');" />
                                                            


														</div>
													</div>
                                        </div>


                                        </div></div></div></div></div>
        </section>
    </div>
</asp:Content>

