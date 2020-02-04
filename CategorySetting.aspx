<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CategorySetting.aspx.cs" Inherits="ColunTableSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Category Setting
            </h1>
            <ol class="breadcrumb">
                <li><a href="index.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Master</li>
                <li class="active">Category Setting</li>

            </ol>
        </section>

        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
            <ContentTemplate>
                <asp:Label ID="hdnID" runat="server" Visible="false" />
                <!-- Form  -->
                <%--<div class="box box-primary" id="ctfrmDet" runat="server" visible="false">

                    <div class="box-body">
                         <div class="col-md-7">
                            <div class="form-group">
                                <label>Category Name 2</label>
                                <asp:TextBox ID="txtCategory2" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-7">
                            <div class="form-group">
                                <label>Category Name 3</label>
                                <asp:TextBox ID="txtCategory3" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-7">
                            <div class="form-group">
                                <label>Category Name 4</label>
                                <asp:TextBox ID="txtCategory4" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-7">
                            <div class="form-group">
                                <label>Category Name 5</label>
                                <asp:TextBox ID="txtCategory5" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                        <asp:Button ID="btnSave" class="btn btn-success pull-right" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" class="btn btn-danger pull-right" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" />
                    </div>
                </div>--%>

                <section class="content">
                    <div class="row">
                        <div class="col-xs-12">

                            <div class="box">
                                <div class="box-header">
                                    <%--  <h3 class="box-title">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add New" class="btn btn-primary" OnClick="btnAdd_Click" /></h3>--%>

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
                                                 <asp:HiddenField ID="hdnCount" Value="0" runat="server" />
                                                <th>Select</th>
                                                <th>ID</th>
                                                <th>Category</th>
                                                <th>Setting Name</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="GV" runat="server" OnItemCommand="rptr_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="hdnID" Value='<%# Eval("ICSettingID")%>' runat="server" />
                                                            <asp:CheckBox ID="ChkQty" Checked='<%# Eval("IsAssigned")%>' runat="server" /></span>
                                                        </td>
                                                        <td><%# Eval("ICSettingID")%> </td>
                                                        <td><%# Eval("TableName")%> </td>
                                                        <td>
                                                            <asp:TextBox ID="txtName" class="form-control" MaxLength="50" Text='<%# Eval("SettingName")%>' runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <%--<asp:ImageButton ID="ImageButton1" runat="server" CommandName="edit"
                                                                ImageUrl="images/edit.png" CommandArgument='<%# Eval("ICSettingID") %>' />--%>

                                                             <asp:LinkButton ID="LinkButton1" CommandName="edit" class="btn btn-primary" CausesValidation="False" CommandArgument='<%# Eval("ICSettingID") %>'
                                                                runat="server"><i class="icon-remove"></i>Add</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                        </tfoot>
                                    </table>
                                </div>
                                <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                    <asp:Button ID="btnSave" class="btn btn-success pull-right" runat="server" Text="Save" OnClick="btnSave_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
