<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="labels.aspx.cs" Inherits="labels" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Labels</h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active"><a href="labels.aspx">Labels</a></li>
            </ol>
        </section>
         <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
        <section class="content">
                    <!-- /.box-header -->
                    <div class="row">
                        <div class="col-xs-12">



                            <div class="box">
                  
                                <div class="box-body">
                                    <div id="divAddAlert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>

                                         <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>
                                    <div id="divSucc" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>



                                   <div>
                                       <table class="table table-bordered table-stripped">
                                           <tr>
                                               <td><asp:DropDownList ID="brand" runat="server" CssClass="form-control select2" DataTextField="C1Name" DataValueField="Col1ID"></asp:DropDownList></td>
                                               <td><asp:DropDownList ID="login" runat="server" CssClass="form-control select2" DataTextField="username" DataValueField="userid"></asp:DropDownList></td>
                                                <td><asp:LinkButton ID="searchLabels" runat="server" OnClick="searchLabels_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-search"></i> Search</asp:LinkButton></td>
                                           </tr>
                                           
                                          
                                       </table>
                                   </div>

                                    <div runat="server" id="dispalyBarcodes" visible="false">
                                       
                                           <table class="table table-bordered table-stripped">
                                                        <thead><tr><th><asp:CheckBox ID="selectDeselect" AutoPostBack="true" runat="server" OnCheckedChanged="selectDeselect_CheckedChanged"/> Check All</th><th>Barcode</th></tr></thead>
                                                         <tbody>
                                                            <asp:Repeater ID="rpt_Barcode" runat="server" >
                                                               <ItemTemplate>
                                                                   <tr>
                                                                       <td>
                                                                           <asp:CheckBox ID="checkBarcode" runat="server" />
                                                                           <asp:Label ID="StockupID" runat="server" Visible="false" Text='<%# Eval("StockupID").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="ArchiveStockupID" runat="server" Visible="false" Text='<%# Eval("ArchiveStockupID").ToString() %>' ></asp:Label>
                                                                      </td>
                                                                       <td>
                                                                           <asp:Label ID="BarcodeNo" runat="server" Visible="false" Text='<%# Eval("BarcodeNo").ToString() %>' ></asp:Label><%# Eval("BarcodeNo").ToString() %>
                                                                       </td>
                                                                        </tr>
                                                                   </ItemTemplate></asp:Repeater>

                                                         </tbody>
                                                    </table>
                                         <div class="box-body table-responsive col-md-12">
                                                            <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                                <asp:Button ID="btnSaveSuccess" class="btn btn-success pull-right btn-sm" runat="server" Text="Success" OnClick="btnSaveSuccess_Click" />

                                                            </div>
                                                        </div>
                                      
                                    </div>
                               
                            </div>
                            <!-- /.box-body -->
                        </div>

                        <!-- /.box -->
                    </div>
                    
                   

                </section>
            </ContentTemplate>
         <Triggers>
        <asp:PostBackTrigger ControlID="btnSaveSuccess" />
              
    </Triggers>
           
        </asp:UpdatePanel>
        </div>
</asp:Content>

