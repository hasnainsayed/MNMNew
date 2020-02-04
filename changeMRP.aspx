<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="changeMRP.aspx.cs" Inherits="changeMRP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Change Barcode MRP
        <!--<small>it all starts here</small>-->
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active"><a href="changeMRP.aspx">Change Barcode MRP</a></li>
            </ol>
        </section>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <section>
                    <!-- Default box -->
                    <div class="box">
                        <div class="box-body">

                            <!-- BOx -->
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Change Barcode MRP</h3>

                                </div>
                                <div class="box-body">
                                    <div class="row table-responsive" id="mainDiv" runat="server">

                                        
                                        <table class="table table-bordered table-striped table-hover">
                                            <tr>
                                                
                                                <th>Barcode/SalesID</th>
                                                <th>Search</th>
                                            </tr>
                                            <tr>
                                               
                                                <td><asp:TextBox ID="searchField" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                <td><asp:LinkButton ID="searchBarcode" runat="server" OnClick="searchBarcode_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-search"></i> Search</asp:LinkButton></td>
                                            </tr>
                                        </table>

                                    </div>
                                    <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>
                                    <div id="divSucc" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>
                                    <div class="row" id="showItem" runat="server" visible="false">
                                        <table class="table table-bordered table-striped">
                                            
                                            <tr>
                                                <th>Barcode</th>
                                                <th>Item Category</th>
                                                <th>Brand</th>
                                                <th>MRP</th>
                                                <th>Save MRP</th>
                                            </tr>
                                           
                                               
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="StockupID" runat="server" Visible="false" Text=''></asp:Label>
                                                            <asp:Label ID="Barcode" runat="server" Text=''></asp:Label>

                                                          
                                                        </td>
                                                      
                                                        <td>
                                                            <asp:Label ID="ItemCategory" runat="server" Text=''></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="C1Name" runat="server" Text=''></asp:Label>
                                                        </td>
                                                         <td>
                                                             <asp:TextBox ID="mrp" runat="server" Text='0' CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="saveMRP" runat="server" CssClass="btn btn-sm btn-danger" OnClick="saveMRP_Click"><i class="fa fa-save"></i> Save MRP</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                              
                                        </table>
                                    </div>
                                    <div class="row" id="noData" runat="server" visible="false">
                                        NO DATA
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

