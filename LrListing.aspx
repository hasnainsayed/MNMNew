<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LrListing.aspx.cs" Inherits="LrListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
    .bora-table{
        text-align: center !important;
    width: 5% !important;
    }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>LR  
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active"><a  href="LrListing.aspx">LR</a></li>
               
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
                                    <label>LR No.</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtlrno" ValidationGroup="save" runat="server" ErrorMessage=" is Mandatory" ForeColor="Red" /><br />
                                    <asp:TextBox ID="txtlrno" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>Description</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtdesc" ValidationGroup="save" runat="server" ErrorMessage=" is Mandatory" ForeColor="Red" /><br />
                                    <asp:TextBox ID="txtdesc" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                            <%--<div class="col-md-6">
                                <div class="form-group">
                                    <label>High IGST </label>
                                 <asp:TextBox ID="higst" runat="server" CssClass="form-control"></asp:TextBox>
                                 <asp:TextBox ID="hsgst" runat="server" CssClass="form-control" Text="0" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="hcgst" runat="server" CssClass="form-control" Text="0" Visible="false"></asp:TextBox>
                                </div>

                            </div>--%>

                    


                             <%--<div class="col-md-6">
                                <div class="form-group">
                                    <label>Low IGST </label>
                                 <asp:TextBox ID="ligst" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="lsgst" runat="server" CssClass="form-control" Text="0" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="lcgst" runat="server" CssClass="form-control" Text="0" Visible="false"></asp:TextBox>
                                </div>

                            </div>--%>

                      
                            <!-- /.col -->
                    </div>
                    <!-- /.box-body -->

                </div>

                    <div class="box-body table-responsive ">
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                        <asp:Button ID="btnSave"  class="btn btn-success pull-right btn-round" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="save"/>
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
                            <asp:Button ID="btnadd" runat="server" Text="Add New LR" class="btn btn-primary btn-sm" OnClick="btnadd_Click" /></h3>
                          </div>
                    <div class="box-body table-responsive">
                         
                        <table id="example111" class="table table-bordered table-striped dtSearch">
                            <thead>
                               <tr>
                                   <th>ID</th>
                                   <th>LR No.</th>
                                   <th>Description</th>
                                   <%--<td>High IGST</td>
                                   <td>Low IGST</td>--%>
                                   <th>Action</th>
                                   <th>Edit</th>
                                   <th colspan="5" style="text-align:center;">Bora Details</th>
                               </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <th class="bora-table">Generated</th>
                                    <th class="bora-table">Loaded</th>
                                    <th class="bora-table">Active</th>
                                    <th class="bora-table">InActive</th>
                                    <th class="bora-table">Total</th>
                                </tr>
                                
                            </thead>
                            <tbody>
                                <asp:Repeater ID="GV" runat="server" OnItemDataBound="GV_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("id")%>
                                            <asp:Label ID="lbId" runat="server" Text='<%# Eval("id").ToString() %>' Visible="false"></asp:Label> 
                                                
                                            </td>
                                            <td><%# Eval("lrno")%></td> 
                                            <td><%# Eval("description")%></td> 
                                            <%--<td><%# Eval("higst")%></td> 
                                            <td><%# Eval("ligst")%></td> --%>
                                            <td>
                                                <div class="btn-group">
                                                <asp:LinkButton ID="statusActive" runat="server"  CssClass="btn btn-sm btn-default" OnClick="statusActive_Click" OnClientClick="return confirm('Do you Want to make it Active ?');">Active</asp:LinkButton>
                                                <asp:LinkButton ID="statusInActive" runat="server"  CssClass="btn btn-sm btn-default" OnClick="statusInActive_Click" OnClientClick="return confirm('Do you Want to make it InActive ?');">InActive</asp:LinkButton>
                                            </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="edit" runat="server" OnClick="edit_Click" Visible="false" CssClass="btn btn-primary btn-sm"><i class="fa fa-pencil"></i> Edit</asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:Label ID="generatedid" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="loadedid" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="activeid" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="inactiveid" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="totalid" runat="server"></asp:Label>
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
