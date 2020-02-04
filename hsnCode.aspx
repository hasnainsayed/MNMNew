<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="hsnCode.aspx.cs" Inherits="hsnCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="content-wrapper">
        <section class="content-header">
            <h1>HSN CODE  
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Masters</li>
                <li class="active"><a  href="hsnCode.aspx">HSN CODE</a></li>
               
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

                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>HSN Code </label>
                                    <asp:TextBox ID="hsncode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>Low High Point </label>
                                
                                  <asp:TextBox ID="lowhighpt" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>High IGST </label>
                                 <asp:TextBox ID="higst" runat="server" CssClass="form-control"></asp:TextBox>
                                 <asp:TextBox ID="hsgst" runat="server" CssClass="form-control" Text="0" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="hcgst" runat="server" CssClass="form-control" Text="0" Visible="false"></asp:TextBox>
                                </div>

                            </div>

                    


                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>Low IGST </label>
                                 <asp:TextBox ID="ligst" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="lsgst" runat="server" CssClass="form-control" Text="0" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="lcgst" runat="server" CssClass="form-control" Text="0" Visible="false"></asp:TextBox>
                                </div>

                            </div>

                      
                            <!-- /.col -->
                    </div>
                    <!-- /.box-body -->

                </div>

                    <div class="box-body table-responsive ">
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                        <asp:Button ID="btnSave"  class="btn btn-success pull-right btn-round" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="grp"/>
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
                            <asp:Button ID="btnadd" runat="server" Text="Add New HSN" class="btn btn-primary btn-sm" OnClick="btnadd_Click" /></h3>
                          </div>
                    <div class="box-body table-responsive">
                         
                        <table id="example111" class="table table-bordered table-striped dtSearch">
                            <thead>
                               <tr>
                                   <td>ID</td>
                                   <td>HSN Code </td>
                                   <td>Low High Pt</td>
                                   <td>High IGST</td>
                                   <td>Low IGST</td>
                                   <td>Action</td>
                               </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="GV" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("hsnid")%>
                                            <asp:Label ID="lblhsnId" runat="server" Text='<%# Eval("hsnid").ToString() %>' Visible="false"></asp:Label> 
                                                
                                            </td>
                                            <td><%# Eval("hsncode")%></td> 
                                            <td><%# Eval("lowhighpt")%></td> 
                                            <td><%# Eval("higst")%></td> 
                                            <td><%# Eval("ligst")%></td> 
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

