<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="addTrader.aspx.cs" Inherits="addTrader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Add Trader
        <!--<small>it all starts here</small>-->
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="invoice.aspx">Invoice</a></li>
                <li class="active">Add Trader</li>
            </ol>
        </section>
        <section class="content">
            <!-- /.box-header -->
            <div class="row">
                <div class="col-xs-12">


                    <div class="box">

                        <div class="box-body table-responsive">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Customer</label>
                                        <asp:DropDownList runat="server" ID="customerId" CssClass="form-control select2" DataValueField="webCustId" DataTextField="custFirstName"></asp:DropDownList>
                                        </div>
                                    </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Bora</label>
                                        <asp:DropDownList runat="server" ID="lotId" CssClass="form-control select2" DataValueField="BagId" DataTextField="BagDescription" AutoPostBack="true" OnSelectedIndexChanged="lotId_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <table class="table table-bordered table-striped">
                                        <tr>
                                            <th>Lot Pieces</th>
                                            <th>Barcoded Pieces</th>
                                            <th>Available Pieces</th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="lotPieces"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="barcodePiece"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="avlPiece"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>

                                          <div class="col-md-12">
                                            <div class="col-md-12">
                                                <asp:Button ID="styleAdd" runat="server" Text="Add New" class="btn bg-purple btn-round margin btn-sm" OnClick="styleAdd_Click" />
                                            </div>
                                            <table class="table table-bordered table-striped">
                                                <thead>
                                                    <tr>
                                                        <th>Style</th>
                                                        <th>MRP/Piece</th>
                                                        <th>Purchase Rate/Piece</th>
                                                        <th>SP</th>
                                                        <th>Quantity</th>
                                                        <th>Piece/Packet</th>
                                                        <th>Remove</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptStyle" runat="server" OnItemDataBound="rptStyle_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="styleDrp" CssClass="form-control select2" runat="server" DataTextField="Title" DataValueField="StyleID"></asp:DropDownList>
                                                                </td>
                                                                <td><asp:TextBox runat="server" CssClass="form-control" ID="mrp" Text='<%# Eval("mrp") %>'></asp:TextBox></td>
                                                                <td><asp:TextBox runat="server" CssClass="form-control" ID="purchaseRate" Text='<%# Eval("purchaseRate") %>'></asp:TextBox></td>
                                                                <td><asp:TextBox runat="server" CssClass="form-control" ID="sp" Text='<%# Eval("sp") %>'></asp:TextBox></td>
                                                                
                                                                <td><asp:TextBox runat="server" CssClass="form-control" ID="quantity" Text='<%# Eval("quantity") %>'></asp:TextBox></td>
                                                                <td>
                                                                    <asp:DropDownList ID="Size" CssClass="form-control select2" runat="server" DataTextField="Size1" DataValueField="SizeID"></asp:DropDownList>
                                                                </td>
                                                                <td><asp:LinkButton runat="server" ID="removeStyle" CssClass="btn btn-sm btn-danger" OnClick="removeStyle_Click"><i class="fa fa-trash"></i> Remove</asp:LinkButton></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>

                                <div class="box-body table-responsive">
                                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                                        <asp:Button runat="server" ID="preview" CssClass="btn btn-info pull-right btn-round" Text="Preview" OnClick="preview_Click"></asp:Button>
                                        <asp:Button runat="server" ID="editTrader" CssClass="btn btn-primary pull-right btn-round" Text="Edit" OnClick="editTrader_Click" Visible="false"></asp:Button>
                                        <asp:Button ID="btnSave" Visible="false" class="btn btn-success pull-right btn-round" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="save" OnClientClick="return confirm('Do you Want to Save ?');" />
                                        <asp:Button ID="btnCancel" class="btn btn-danger pull-right btn-round" runat="server" Text="Cancel" OnClick="btnCancel_Click" />

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

