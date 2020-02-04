<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ItemStyle.aspx.cs" Inherits="ItemStyle" %>

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
    <asp:HiddenField ID="hdnItemCatID" runat="server" />

    <div class="content-wrapper">
        <!-- Content Header (Page header) -->

        <section class="content-header">
            <h1>Item Style
            </h1>
            <ol class="breadcrumb">
                <li><a href="index.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Master</li>
                <li class="active">Item Style</li>

            </ol>
        </section>

        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
            <ContentTemplate>
                <!-- Form  -->
                <section class="content">
                    <div class="row">
                        <div class="col-xs-12">

                            <div class="box box-primary" id="dvItemCat" runat="server" visible="true" style="margin-bottom: 0px !important">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Item Category</h3>
                                </div>
                                <div class="box-body">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Select Item Category</label>
                                            <asp:DropDownList ID="ddlItemcategory" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlItemcategory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div id="dvcat2" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                Select
                                                <asp:Label ID="lblCat2" runat="server" Text=""></asp:Label></label></label>
                                            <asp:DropDownList ID="ddlCat2" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCat2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div id="dvcat3" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                Select
                                                <asp:Label ID="lblCat3" runat="server" Text=""></asp:Label></label></label>
                                            <asp:DropDownList ID="ddlCat3" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCat3_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div id="dvcat4" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                Select
                                                <asp:Label ID="lblCat4" runat="server" Text=""></asp:Label></label></label>
                                            <asp:DropDownList ID="ddlCat4" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCat4_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div id="dvcat5" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                Select
                                                <asp:Label ID="lblCat5" runat="server" Text=""></asp:Label></label></label>
                                            <asp:DropDownList ID="ddlCat5" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="box box-warning" id="ctfrmDet" runat="server" visible="true" style="margin-bottom: 0px !important">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Default Controls</h3>
                                </div>
                                <div class="box-body">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Item Title</label>
                                            <asp:RequiredFieldValidator runat="server" ForeColor="Red" ID="RequiredFieldValidator1" ValidationGroup="validate" ErrorMessage="Please enter name" ControlToValidate="txtTitle" Display="Dynamic" />
                                            <asp:TextBox ID="txtTitle" runat="server" ValidationGroup="validate" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div id="img" runat="server" visible="false" class="col-md-6" >
                                        <div class="form-group">
                                            <label>Image</label>
                                            <%--<asp:TextBox ID="txtImage" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                            <asp:FileUpload ID="fluImage" multiple="multiple" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <%--<div class="col-md-4">
                            <div class="form-group">
                                <label>Select Vendor</label>
                                <asp:DropDownList ID="ddlVendor" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                            </div>

                            <div class="box box-warning" id="dvItemCol" runat="server" visible="true">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Item Controls</h3>
                                </div>
                                <div class="box-body">

                                    <div id="col1" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                Select
                                    <asp:Label ID="lblcol1" runat="server" Text=""></asp:Label></label></label>
                                <asp:DropDownList ID="ddlcol1" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control">
                                </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div id="col2" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                Select
                                    <asp:Label ID="lblcol2" runat="server" Text=""></asp:Label></label></label>
                                <asp:DropDownList ID="ddlcol2" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control">
                                </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div id="col3" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                Select
                                    <asp:Label ID="lblcol3" runat="server" Text=""></asp:Label></label></label>
                                <asp:DropDownList ID="ddlcol3" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control">
                                </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div id="col4" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                Select
                                    <asp:Label ID="lblcol4" runat="server" Text=""></asp:Label></label></label>
                                <asp:DropDownList ID="ddlcol4" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control">
                                </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div id="col5" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                Select
                                    <asp:Label ID="lblcol5" runat="server" Text=""></asp:Label></label></label>
                                <asp:DropDownList ID="ddlcol5" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control">
                                </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div id="col6" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                Select
                                    <asp:Label ID="lblcol6" runat="server" Text=""></asp:Label></label></label>
                                <asp:DropDownList ID="ddlcol6" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control">
                                </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div id="col7" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                Select
                                    <asp:Label ID="lblcol7" runat="server" Text=""></asp:Label></label></label>
                                <asp:DropDownList ID="ddlcol7" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control">
                                </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div id="col8" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                Select
                                    <asp:Label ID="lblcol8" runat="server" Text=""></asp:Label></label></label>
                                <asp:DropDownList ID="ddlcol8" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control">
                                </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div id="col9" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                Select
                                    <asp:Label ID="lblcol9" runat="server" Text=""></asp:Label></label></label>
                                <asp:DropDownList ID="ddlcol9" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control">
                                </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div id="col10" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                Select
                                    <asp:Label ID="lblcol10" runat="server" Text=""></asp:Label></label></label>
                                <asp:DropDownList ID="ddlcol10" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control">
                                </asp:DropDownList>
                                        </div>
                                    </div>

                                    <%--Text Control ( 20 Text Box)--%>

                                    <div id="ctrl1" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl1" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl1" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl2" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl2" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl2" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl3" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl3" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl3" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl4" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl4" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl4" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl5" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl5" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl5" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl6" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl6" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl6" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl7" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl7" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl7" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl8" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl8" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl8" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl9" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl9" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl9" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl10" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl10" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl10" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl11" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl11" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl11" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl12" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl12" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl12" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl13" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl13" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl13" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl14" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl14" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl14" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl15" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl15" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl15" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl16" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl16" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl16" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl17" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl17" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl17" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl18" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl18" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl18" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl19" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl19" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl19" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="ctrl20" visible="false" runat="server" class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblctrl20" runat="server" Text=""></asp:Label></label></label>
                                <asp:TextBox ID="txtctrl20" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" />
                                <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                    <asp:Button ID="btnSave" Visible="true" class="btn btn-success pull-right" ValidationGroup="validate" data-toggle="modal" data-target="#modal1" runat="server" CausesValidation="true" Text="Save" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnUpdate" Visible="false" class="btn btn-warning pull-right" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                                    <asp:Button ID="btnCancel" class="btn btn-danger pull-right" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" />

                                    <%--<button type="button" class="btn btn-default" data-toggle="modal" data-target="#modal1">Launch Default Modal</button>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%-- </section>

                <section class="content">--%>
                    <div id="rptr" runat="server" visible="false" class="row" style="margin-top: 15px;">
                        <div class="col-xs-12">

                            <div class="box">
                                <div class="box-header">
                                    <h3 class="box-title">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add New" class="btn btn-primary" OnClick="btnAdd_Click" /></h3>

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
                                <div class="box-body">
                                    <table id="example111" class="table table-bordered table-striped dtSearch">
                                        <thead>
                                            <tr>
                                                <asp:HiddenField ID="hdnID" runat="server" />
                                                <th>ID</th>
                                                <th>Vendor Name</th>
                                                <th>Contact No</th>
                                                <th>Email</th>
                                                <th>City</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater ID="GV" runat="server" OnItemCommand="rptr_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("VendorID")%> </td>
                                                        <td><%# Eval("VendorName")%> </td>
                                                        <td><%# Eval("Contact")%> </td>
                                                        <td><%# Eval("Email")%> </td>
                                                        <td><%# Eval("City")%> </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="edit"
                                                                ImageUrl="images/edit.png" CommandArgument='<%# Eval("VendorID") %>' />

                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="modal fade" id="modal1" ClientIDMode="Static"  runat="server">
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

                    <asp:Button ID="btnUpload" class="btn btn-primary" runat="server" Text="Upload"
                        OnClick="btnUpload_Click" />
                </div>
                <asp:Label ID="lblMessage" runat="server" />
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>

