<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="addToStock.aspx.cs" Inherits="addToStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Add Stock
        <!--<small>it all starts here</small>-->
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>

                <li class="active">Add Stock</li>
            </ol>
        </section>
        <section class="content">
            <!-- /.box-header -->
            <div class="row">
                <div class="col-xs-12">


                    <div class="box">

                        <div class="box-body table-responsive">
                            <div class="row">
                                <table class="table table-bordered table-striped">
                        <tr>
                            <th>Lot Pieces</th><th>Barcoded Pieces</th><th>Available Pieces</th>
                        </tr>
                            <tr>
                                <td><asp:Label runat="server" ID="lotPieces"></asp:Label></td>
                                <td><asp:Label runat="server" ID="barcodePiece"></asp:Label></td>
                                <td><asp:Label runat="server" ID="avlPiece"></asp:Label></td>
                            </tr>
                       </table>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Barcode No</label>
                                        <asp:TextBox ID="barcodeNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Quantity</label>
                                        <asp:TextBox ID="quantity" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Purchase Rate</label>
                                        <asp:TextBox ID="purchaseRate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                     <div>
                               <asp:label runat="server" ID="successfailure" Text="" Visible="false" style="color:crimson;text-align:center;"></asp:label>
                           </div>
                                    <div class="form-group">

                                        <table class="table table-bordered table-striped">
                                            <%-- <asp:label runat="server" id="scrollDownLotRpt"></asp:label> --%>
                                            <tr>
                                                <th width="2%">Select </th>
                                                <th>Bora</th>
                                            </tr>
                                            <asp:Repeater ID="lot_RPT" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lotLblHdn" Visible="false" runat="server" Text='<%# Eval("BagId").ToString() %>'></asp:Label>
                                                            <asp:Label ID="lotPiece" Visible="false" runat="server" Text='<%# Eval("totalPiece").ToString() %>'></asp:Label>
                                                            <asp:RadioButton ID="lotRadio" AutoPostBack="true" runat="server" OnCheckedChanged="lotRadio_CheckedChanged"></asp:RadioButton>
                                                        </td>
                                                        <td><%# Eval("BagDescription").ToString() %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                </div>
                                

                               
                                <div class="box-body table-responsive">
                                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                                        <asp:Button ID="btnSave" class="btn btn-success pull-right btn-round" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="save" OnClientClick="return confirm('Do you Want to Save ?');" />

                                    </div>
                                </div>
                            </div>
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

