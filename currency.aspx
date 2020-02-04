<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="currency.aspx.cs" Inherits="centre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper">
        <section class="content-header">
            <h1>Currency
        <!--<small>it all starts here</small>-->
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Masters</a></li>
                <li class="active">Currency</li>
            </ol>
        </section>
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                 <div class="box box-primary" id="devCapone" runat="server" visible="false">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title"></h3>

                       
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                 <div class="col-md-6">
                                  
                                        <Label>Currency Name</Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="currencyName" ValidationGroup="save" runat="server" ErrorMessage=" is Mandatory" ForeColor="Red" /><br />

                                        <asp:TextBox ID="currencyName" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                     <div class="col-md-6">
                                 
                                        <Label>Currecncy Code</Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="currencyCode" ValidationGroup="save" runat="server" ErrorMessage="Selection  is Mandatory" ForeColor="Red" /><br />

                                                  <asp:TextBox ID="currencyCode" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                       
                    <!-- /.box-body -->

                </div>

                    <div class="box-body table-responsive ">
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                        <asp:Button ID="btnSave"  class="btn btn-success pull-right btn-round" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="save" OnClientClick="return confirm('Do you Want to Save ?');"/>
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
                            <asp:Button ID="btnadd" runat="server" Text="Add New Currency" class="btn btn-primary btn-sm" OnClick="btnadd_Click" /></h3>
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
                                   <td>Currency </td>  
                                   <td>Code </td>   
                                   <td>Edit</td>                                  
                               </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="GV" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("currencyId")%>
                                            <asp:Label ID="currencyId" runat="server" Text='<%# Eval("currencyId").ToString() %>' Visible="false"></asp:Label> 
                                                
                                            </td>
                                            <td><%# Eval("currencyName")%></td> 
                                            <td><%# Eval("currencyCode")%></td> 
                                           
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
                            </asp:UpdatePanel>
        </div>
</asp:Content>

