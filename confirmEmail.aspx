<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="confirmEmail.aspx.cs" Inherits="confirmEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Confirmation Email
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Communication Mgmt</a></li><li><a href="#">Emails</a></li>
                <li class="active"><a href="confirmEmail.aspx">Confirm Email</a></li>
            </ol>
        </section>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <section class="content">
            	<!-- Default box -->
					<div class="box">
						<div class="box-body">
							<!-- BOx -->
							<div class="box box-primary">
								<div class="box-header with-border">
									<h3 class="box-title">Update Confirm Email</h3>
                                </div>
								<!-- /.box-header -->
								<div class="box-body table-responsive">
									<div class="row">
                                        <div id="divAddAlert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>

                                         <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>
                                        
                                        <asp:Label ID="emailId" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="Label1" runat="server" Text="" Visible="false"></asp:Label>  
                            <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Sender</label>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="senders"
            ValidationGroup="save" runat="server" ErrorMessage="Sender Email is required." forecolor="Red" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="senders"
    ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
    Display = "Dynamic" ValidationGroup="save" ErrorMessage = "Invalid Sender Email address"/>
                                        <asp:TextBox ID="senders" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                        <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Subject</label>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="subject"
            ValidationGroup="save" runat="server" ErrorMessage="Subject is required." forecolor="Red" /><br />
                                        <asp:TextBox ID="subject" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                           <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Body</label>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="body"
            ValidationGroup="save" runat="server" ErrorMessage="Body is required." forecolor="Red" /><br />
                                        <asp:TextBox ID="body" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                                         <div class="col-md-12">
                                            <div class="box-body table-responsive ">
														<div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                            <asp:Button ID="cancel" runat="server" Text="Cancel" CssClass="btn btn-sm btn-danger pull-right" OnClick="cancel_Click" OnClientClick="return confirm('Do you Want to Cancel ?');" />
															<asp:Button ID="save" runat="server" Text="Save" CssClass="btn btn-sm btn-success pull-right" OnClick="save_Click" ValidationGroup="save"  />
                                                            


														</div>
													</div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                     </div>
                </div>
            </section>
            </ContentTemplate>
        </asp:UpdatePanel>
        

    </div>
</asp:Content>

