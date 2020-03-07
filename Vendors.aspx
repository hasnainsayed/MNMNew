<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Vendors.aspx.cs" Inherits="Vendors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Vendor  
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Masters</li>
                <li class="active"><a  href="Vendors.aspx">Vendor</a></li>
               
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
               
                         <div class="row">
                              <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Succesfully Added</h5>

                                        </div>
                             <div class="col-md-4">
                                <div class="form-group">
                                    <label>Vendor Name </label>
                                    <asp:TextBox ID="VendorName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                             <div class="col-md-4">
                                <div class="form-group">
                                    <label>Contact No.</label>
                                
                                  <asp:TextBox ID="Contact" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Email Address</label>
                                
                                  <asp:TextBox ID="Email" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>

                             <div class="col-md-4">
                                <div class="form-group">
                                    <label>City </label>
                                
                                  <asp:TextBox ID="City" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>

                             <div class="col-md-4">
                                <div class="form-group">
                                    <label>Address </label>
                                
                                  <asp:TextBox ID="vAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>

                             <div class="col-md-4">
                                <div class="form-group">
                                    <label>GST No. </label>
                                
                                  <asp:TextBox ID="gstin" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>

                             <div class="col-md-4">
                                <div class="form-group">
                                    <label>Brand</label>
                                    <asp:DropDownList ID="drpsupervendor" runat="server" CssClass="form-control select2" DataTextField="C1Name" DataValueField="Col1ID"></asp:DropDownList>
                                </div>

                            </div>
                              <div class="col-md-4">
                                <div class="form-group">
                                   <Label>Is Tax</Label>  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rbltax" ValidationGroup="save" runat="server" ErrorMessage=" is Mandatory" ForeColor="Red" /><br />
                                      <asp:RadioButtonList ID="rbltax" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Text="Yes" Value="1"  ></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"  Selected="True" runat="server"  style="margin-left: 15px;"></asp:ListItem>
                                       </asp:RadioButtonList>
                                </div>

                            </div>
                            
                             
                            <!-- /.col -->
                    </div>
                    <!-- /.box-body -->

                </div>

                    <div class="box-body table-responsive ">
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                        <asp:Button ID="btnSave"  class="btn btn-success pull-right btn-round" ValidationGroup="save" runat="server" Text="Save" OnClick="btnSave_Click"/>
                        <asp:Button ID="btnCancel"  class="btn btn-danger pull-right btn-round" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
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
                            <asp:Button ID="btnadd" runat="server" Text="Add New Vendor" class="btn btn-primary btn-sm" OnClick="btnadd_Click" /></h3>
                          
                                      <div id="divAddAlert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Succesfully Added</h5>

                                        </div>

                                        <div id="divUpdAlert" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Succesfully Updated</h5>
                                        </div>

                        <div id="divErrorAlert" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Transaction Failed</h5>

                                        </div>
                    </div>
                    <div class="box-body table-responsive">
                         
                        <table id="example111" class="table table-bordered table-striped dtSearch">
                            <thead>
                               <tr>
                                   <td>ID</td>
                                   <td>Vendor </td>
                                   <td>Contact No</td>
                                   <td>Email Address</td>
                                   <td>City</td>
                                   <td>GST IN</td>
                                
                                   <td>Edit</td>
                                   
                               </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="GV" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("VendorID")%>
                                            <asp:Label ID="lblVendorID" runat="server" Text='<%# Eval("VendorID").ToString() %>' Visible="false"></asp:Label> 
                                                
                                            </td>
                                            <td><%# Eval("VendorName")%></td> 
                                            <td><%# Eval("Contact")%></td> 
                                            <td><%# Eval("Email")%></td> 
                                            <td><%# Eval("City")%></td> 
                                            <td><%# Eval("gstin")%></td> 
                                         
                                            <td>
                                                <asp:LinkButton ID="edit" runat="server" OnClick="edit_Click" CssClass="btn btn-warning btn-sm"><i class="fa fa-pencil"></i> Edit</asp:LinkButton>
                                               
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
                            </asp:UpdatePanel>
    </div>
</asp:Content>

