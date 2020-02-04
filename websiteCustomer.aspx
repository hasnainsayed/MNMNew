<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="websiteCustomer.aspx.cs" Inherits="websiteCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Customer List
        <!--<small>it all starts here</small>-->
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Customer List</li>                
            </ol>
        </section>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <section>
                    <div class="box">
                        <div class="box-body">
                            <!-- BOx -->
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Customer List</h3>
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">
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
                                    <div runat="server" id="showData" class=" table-responsive">                                        
                                        <table class="table table-bordered table-stripped dtSearch">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Name</th>
                                                    <th>Email Address</th>
                                                    <th>Phone No</th>                                                    
                                                    <th>Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptCustomer" runat="server" OnItemDataBound="rptCustomer_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><asp:Label runat="server" ID="webCustId" Visible="false" Text='<%# Eval("webCustId") %>'></asp:Label><%# Eval("webCustId") %></td>
                                                            <td><%# Eval("custFirstName") %></td>
                                                            <td><%# Eval("emailAddress") %></td>
                                                            <td><%# Eval("phoneNo") %></td>
                                                            <td>
                                                                <div class="btn-group">
                                                                  <asp:LinkButton ID="statusOn" runat="server" CssClass="btn btn-sm btn-default" OnClick="statusOn_Click">Active</asp:LinkButton>
                                                                  <asp:LinkButton ID="statusOff" runat="server" CssClass="btn btn-sm btn-default" OnClick="statusOff_Click">In Active</asp:LinkButton>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>

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

