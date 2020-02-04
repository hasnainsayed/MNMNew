<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="sellOnWebsite.aspx.cs" Inherits="sellOnWebsite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Sell</h1>
            <ol class="breadcrumb">
                <li><a href="index.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Administrator</li><li>Web Controls</li>
                <li class="active">Sell</li>
            </ol>
        </section>
        <section class="content">
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

                                         <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>
                                    <div id="divSucc" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>

                                    <div id="divUpdAlert" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saving Failed</h5>

                                    </div>
                        <table id="example111" class="table table-bordered table-striped dtSearch">
                            <thead>
                               <tr>
                                   <td>Sr No.</td>
                                   <td>Title</td>
                                   <td>Priotity</td>
                                   <td>Edit</td>
                               </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="GV" runat="server" >
                                    <ItemTemplate>
                                        <tr>
                                           <td>
                                               <asp:Label ID="sellId" Visible="false" runat="server" Text='<%# Eval("sellId").ToString() %>'></asp:Label><%# Eval("Row_Number").ToString() %></td>
                                           <td><%# Eval("displayTitle").ToString() %></td>
                                            <td><%# Eval("priorities").ToString() %></td>
                                           <td>
                                               <asp:LinkButton ID="editWebsite" runat="server" CssClass="btn btn-sm btn-primary" OnClick="editWebsite_Click"><i class="fa fa-pencil"></i> Edit</asp:LinkButton>
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
     </div>
</asp:Content>
