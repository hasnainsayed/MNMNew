<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StockUp.aspx.cs" Inherits="StockUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .example-modal .modal {
            position: relative;
            top: auto;
            bottom: auto;
            right: auto;
            left: auto;
            display: block;
            z-index: 1;
        }

        .example-modal .modal {
            background: transparent !important;
        }
    </style>
    <script src="MultipleUpload/jquery-1.8.2.js"></script>
    <script src="MultipleUpload/jquery.MultiFile.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnItemStyleID" runat="server" />

    <div class="content-wrapper">
        <!-- Content Header (Page header) -->

        <section class="content-header">
            <h1>StockUp
            </h1>
            <ol class="breadcrumb">
                <li><a href="index.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Master</li>
                <li class="active">StockUp</li>

            </ol>
        </section>

        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
            <ContentTemplate>
                <!-- Form  -->
                <section class="content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box box-warning" style="margin-bottom: 0px !important">
                                <div class="box-header with-border">
                                    <h3 class="box-title">StockUp</h3>
                                </div>
                                <div class="box-body">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Select Item Category</label>
                                            <asp:DropDownList ID="ddlItemcategory" runat="server" DataTextField="name" DataValueField="id" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlItemcategory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div id="dvrbd1" visible="false" runat="server" class="col-md-2">
                                        <div class="form-group">
                                            <label>
                                                Search By Fixed Values
                                            </label>
                                            <asp:RadioButton ID="rbdFixedvalue" CssClass="form-control" runat="server" AutoPostBack="true" OnCheckedChanged="rbdFixedvalue_CheckedChanged" />
                                        </div>
                                    </div>
                                    <div id="dvrbd2" visible="false" runat="server" class="col-md-2">
                                        <div class="form-group">
                                            <label>
                                                Wild card search
                                            </label>
                                            <asp:RadioButton ID="rbdwildcard" CssClass="form-control" runat="server" AutoPostBack="true" OnCheckedChanged="rbdwildcard_CheckedChanged" />
                                        </div>
                                    </div>
                                    <div id="dvrbd3" visible="false" runat="server" class="col-md-2">
                                        <div class="form-group">
                                            <label>
                                                Show All
                                            </label>
                                            <asp:RadioButton ID="rbdShowall" CssClass="form-control" runat="server" AutoPostBack="true" OnCheckedChanged="rbdShowall_CheckedChanged" />
                                        </div>
                                    </div>
                                    <div id="dvddl" visible="false" runat="server" class="col-md-3">
                                        <div class="form-group">
                                            <label>Select Column Table</label>
                                            <asp:DropDownList ID="ddlColSettings" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlColSettings_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div id="dvcat" visible="false" runat="server" class="col-md-3">
                                        <div class="form-group">
                                            <label>
                                                Select
                                                <asp:Label ID="lblCol" runat="server" Text=""></asp:Label></label></label>
                                            <asp:DropDownList ID="ddlCol" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCol_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div id="dvtxt" visible="false" runat="server" class="col-md-3">
                                        <div class="form-group">
                                            <label>Search</label>
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <h3 class="box-title" style="float: right;">
                                        <asp:Button ID="btnAddStyle" runat="server" Visible="false" Text="Add" class="btn btn-primary" OnClick="btnAddStyle_Click" />

                                        <asp:Button ID="btnSearch" runat="server" Visible="false" Text="Search" class="btn btn-warning" OnClick="btnSearch_Click" /></h3>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="row" id="dvItemCat" runat="server" visible="false" style="margin-top: 15px;">
                        <div class="col-xs-12">

                            <div class="box">
                                <div class="box-header">
                                    <div class="box-body table-responsive " style="border: none !important">
                                        <div id="divAddAlert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Succesfully Added</h5>
                                        </div>

                                        <div id="divUpdAlert" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Succesfully Updated</h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-body table-responsive">
                                    <table id="example1" class="table table-bordered table-striped dtSearch">
                                        <thead>
                                            <tr>
                                                <asp:HiddenField ID="hdnID" runat="server" />
                                                <th>RFL Qty</th>
                                                <th>Reject Qty</th>
                                                <th>Category</th>
                                                <th>Item Title</th>
                                                <th id="col1" runat="server" visible="false">
                                                    <asp:Label ID="lblcol1" runat="server" Text=""></asp:Label></th>
                                                <th id="col2" runat="server" visible="false">
                                                    <asp:Label ID="lblcol2" runat="server" Text=""></asp:Label></th>
                                                <th id="col3" runat="server" visible="false">
                                                    <asp:Label ID="lblcol3" runat="server" Text=""></asp:Label></th>
                                                <th id="col4" runat="server" visible="false">
                                                    <asp:Label ID="lblcol4" runat="server" Text=""></asp:Label></th>
                                                <th id="col5" runat="server" visible="false">
                                                    <asp:Label ID="lblcol5" runat="server" Text=""></asp:Label></th>
                                                <th id="col6" runat="server" visible="false">
                                                    <asp:Label ID="lblcol6" runat="server" Text=""></asp:Label></th>
                                                <th id="col7" runat="server" visible="false">
                                                    <asp:Label ID="lblcol7" runat="server" Text=""></asp:Label></th>
                                                <th id="col8" runat="server" visible="false">
                                                    <asp:Label ID="lblcol8" runat="server" Text=""></asp:Label></th>
                                                <th id="col9" runat="server" visible="false">
                                                    <asp:Label ID="lblcol9" runat="server" Text=""></asp:Label></th>
                                                <th id="col10" runat="server" visible="false">
                                                    <asp:Label ID="lblcol10" runat="server" Text=""></asp:Label></th>

                                                <th id="control1" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol1" runat="server" Text=""></asp:Label></th>
                                                <th id="control2" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol2" runat="server" Text=""></asp:Label></th>
                                                <th id="control3" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol3" runat="server" Text=""></asp:Label></th>
                                                <th id="control4" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol4" runat="server" Text=""></asp:Label></th>
                                                <th id="control5" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol5" runat="server" Text=""></asp:Label></th>
                                                <th id="control6" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol6" runat="server" Text=""></asp:Label></th>
                                                <th id="control7" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol7" runat="server" Text=""></asp:Label></th>
                                                <th id="control8" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol8" runat="server" Text=""></asp:Label></th>
                                                <th id="control9" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol9" runat="server" Text=""></asp:Label></th>
                                                <th id="control10" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol10" runat="server" Text=""></asp:Label></th>
                                                <th id="control11" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol11" runat="server" Text=""></asp:Label></th>
                                                <th id="control12" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol12" runat="server" Text=""></asp:Label></th>
                                                <th id="control13" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol13" runat="server" Text=""></asp:Label></th>
                                                <th id="control14" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol14" runat="server" Text=""></asp:Label></th>
                                                <th id="control15" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol15" runat="server" Text=""></asp:Label></th>
                                                <th id="control16" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol16" runat="server" Text=""></asp:Label></th>
                                                <th id="control17" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol17" runat="server" Text=""></asp:Label></th>
                                                <th id="control18" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol18" runat="server" Text=""></asp:Label></th>
                                                <th id="control19" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol19" runat="server" Text=""></asp:Label></th>
                                                <th id="control20" runat="server" visible="false">
                                                    <asp:Label ID="lblcontrol20" runat="server" Text=""></asp:Label></th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater ID="GV" runat="server" OnItemDataBound="rptr_ItemBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="LinkButton1" CommandName="edit" class="btn btn-primary" CausesValidation="False" CommandArgument='<%# Eval("StyleID") %>'
                                                                runat="server"><i class="icon-remove"></i>RFL +</asp:LinkButton></td>
                                                        <td>
                                                            <asp:LinkButton ID="LinkButton2" CommandName="edit" class="btn btn-danger" CausesValidation="False" CommandArgument='<%# Eval("StyleID") %>'
                                                                runat="server"><i class="icon-remove"></i>Reject +</asp:LinkButton></td>
                                                        <td><%# Eval("ItemCategory")%> </td>
                                                        <td><%# Eval("Title")%> </td>

                                                        <td id="col1" runat="server" visible="false"><%# Eval("C1Name")%> </td>
                                                        <td id="col2" runat="server" visible="false"><%# Eval("C2Name")%> </td>
                                                        <td id="col3" runat="server" visible="false"><%# Eval("C3Name")%> </td>
                                                        <td id="col4" runat="server" visible="false"><%# Eval("C4Name")%> </td>
                                                        <td id="col5" runat="server" visible="false"><%# Eval("C5Name")%> </td>
                                                        <td id="col6" runat="server" visible="false"><%# Eval("C6Name")%> </td>
                                                        <td id="col7" runat="server" visible="false"><%# Eval("C7Name")%> </td>
                                                        <td id="col8" runat="server" visible="false"><%# Eval("C8Name")%> </td>
                                                        <td id="col9" runat="server" visible="false"><%# Eval("C9Name")%> </td>
                                                        <td id="col10" runat="server" visible="false"><%# Eval("C10Name")%> </td>

                                                        <td id="control1" runat="server" visible="false"><%# Eval("Control1")%> </td>
                                                        <td id="control2" runat="server" visible="false"><%# Eval("Control2")%> </td>
                                                        <td id="control3" runat="server" visible="false"><%# Eval("Control3")%> </td>
                                                        <td id="control4" runat="server" visible="false"><%# Eval("Control4")%> </td>
                                                        <td id="control5" runat="server" visible="false"><%# Eval("Control5")%> </td>
                                                        <td id="control6" runat="server" visible="false"><%# Eval("Control6")%> </td>
                                                        <td id="control7" runat="server" visible="false"><%# Eval("Control7")%> </td>
                                                        <td id="control8" runat="server" visible="false"><%# Eval("Control8")%> </td>
                                                        <td id="control9" runat="server" visible="false"><%# Eval("Control9")%> </td>
                                                        <td id="control10" runat="server" visible="false"><%# Eval("Control10")%> </td>
                                                        <td id="control11" runat="server" visible="false"><%# Eval("Control11")%> </td>
                                                        <td id="control12" runat="server" visible="false"><%# Eval("Control12")%> </td>
                                                        <td id="control13" runat="server" visible="false"><%# Eval("Control13")%> </td>
                                                        <td id="control14" runat="server" visible="false"><%# Eval("Control14")%> </td>
                                                        <td id="control15" runat="server" visible="false"><%# Eval("Control15")%> </td>
                                                        <td id="control16" runat="server" visible="false"><%# Eval("Control16")%> </td>
                                                        <td id="control17" runat="server" visible="false"><%# Eval("Control17")%> </td>
                                                        <td id="control18" runat="server" visible="false"><%# Eval("Control18")%> </td>
                                                        <td id="control19" runat="server" visible="false"><%# Eval("Control19")%> </td>
                                                        <td id="control20" runat="server" visible="false"><%# Eval("Control20")%> </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                        </tfoot>
                                    </table>
                                </div>

                                <div id="loader" runat="server" visible="true" class="overlay">
                                    <i class="fa fa-refresh fa-spin"></i>
                                </div>

                            </div>
                        </div>
                    </div>
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="modal fade" id="modal1" clientidmode="Static" runat="server">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Default Modal</h4>
                </div>
                <div class="modal-body">
                    <asp:FileUpload ID="file_upload" class="multi" runat="server" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>

                    <asp:Button ID="btnUpload" class="btn btn-primary" runat="server" Text="Upload" />
                </div>
                <asp:Label ID="lblMessage" runat="server" />
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>

