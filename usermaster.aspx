<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="usermaster.aspx.cs" Inherits="usermaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Users  
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Masters</li>
                <li class="active"><a  href="usermaster.aspx">Users</a></li>
               
            </ol>
        </section>
     <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                 <div class="box box-primary" id="devCapone" runat="server" visible="false">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title"></h3>

                       <%-- <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                        </div>--%>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                <div id="divErr" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saving Failed</h5>

                                    </div>
                         <div class="row">

                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>User Name </label>
                                    <asp:TextBox ID="username" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>

                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>Password </label>
                                    <asp:TextBox ID="password" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>

                     <div class="col-md-6">
                                <div class="form-group">
                                    <label>Role </label>
                                    <asp:DropDownList ID="userrole" runat="server" CssClass="form-control select2" DataValueField="roleId" DataTextField="roleName"></asp:DropDownList>
                                </div>

                            </div>

                              <div class="col-md-6">
                                <div class="form-group">
                                    <label>User Type </label>
                                    <asp:DropDownList ID="uType" runat="server" CssClass="form-control select2">
                                        <asp:ListItem Value="1">WareHouse</asp:ListItem>
                                        <asp:ListItem Value="2">Sales Channel</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>

                              <div class="col-md-6">
                                <div class="form-group">
                                    <label>Physical Location </label>
                                    <asp:DropDownList ID="physicalLocation" runat="server" CssClass="form-control select2" DataValueField="LocationID" DataTextField="Location"></asp:DropDownList>
                                </div>

                            </div>
                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>Image </label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" /> </br>
                                    <asp:Image ID="image1Display" runat="server" ImageUrl="..." Visible="false" Height="100px" Width="100px" />
                                                               
                                </div>

                            </div>
                             
                      
                            <!-- /.col -->
                    </div>
                    <!-- /.box-body -->

                </div>

                    <div class="box-body table-responsive ">
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                        <asp:Button ID="btnSave"  class="btn btn-success pull-right btn-sm" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel"  class="btn btn-danger pull-right btn-sm" runat="server" Text="Cancel" OnClick="btnCancel_Click"/>
                    </div>
                </div>
                <!-- /.box -->
            </div>
            </div>
     
        
<section class="content">
        <!-- /.box-header -->
        <div class="row">
            <div class="col-xs-12">
               
            <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label> 
                
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">
                            <asp:Button ID="btnadd" runat="server" Text="Add New" class="btn btn-primary btn-sm" OnClick="btnadd_Click" /></h3>
                          </div>
                    <div class="box-body table-responsive">
                           <div id="divAddAlert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                    </div>

                                    <div id="divUpdAlert" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saving Failed</h5>

                                    </div>
                        <table id="example111" class="table table-bordered table-striped dtSearch">
                            <thead>
                               <tr>
                                   <td>ID</td>
                                   <td>UserName </td>
                                   
                                   <td>Action</td>
                               </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="GV" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("userid")%>
                                            <asp:Label ID="userid" runat="server" Text='<%# Eval("userid").ToString() %>' Visible="false"></asp:Label> 
                                                
                                            </td>
                                            <td><%# Eval("username")%></td> 
                                          
                                            <td>
                                                <asp:LinkButton ID="edit" runat="server" OnClick="edit_Click" CssClass="btn btn-primary btn-sm"><i class="fa fa-pencil"></i> Edit</asp:LinkButton>
                                               
                                            </td>
                                           

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>



                            </tbody>
                                <tfoot>
                            </tfoot>
                        </table>
                         
                    </div>
                    <!-- /.box-body -->
                </div>
   
                <!-- /.box -->
            </div>
            <!-- /.col -->
        </div>
   </section>
    </ContentTemplate>
         <Triggers>
				<asp:PostBackTrigger ControlID="btnSave"/>
		 </Triggers>
                            </asp:UpdatePanel>
    </div>
</asp:Content>

