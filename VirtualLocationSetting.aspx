<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VirtualLocationSetting.aspx.cs" Inherits="VirtualLocationSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnLocationID" runat="server" />
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Virtual Location Settings
            </h1>
            <ol class="breadcrumb">
                <li><a href="index.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Master</li>
                <li class="active">Virtual Location Settings</li>

            </ol>
        </section>
        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
            <ContentTemplate>
                <asp:Label ID="hdnID" runat="server" Visible="false" />
                <!-- Form  -->
                <section class="content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box">
                                <div class="box-header">
                                     <h3 class="box-title">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add New" class="btn btn-primary" OnClick="btnAdd_Click" /></h3>
                                        <asp:Button ID="btnBack" runat="server" Text="Back" class="btn btn-primary" CausesValidation="False" OnClick="btnBack_Click" /></h3>

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
                                                <th>Select</th>
                                                <th>ID</th>
                                                <th>Control</th>
                                                <th>Setting Name</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="GV" runat="server" OnItemDataBound="rptr_ItemBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="hdnID" Value='<%# Eval("StyleCSID")%>' runat="server" />
                                                            <asp:HiddenField ID="hdnIsAssigned" Value='<%# Eval("IsAssigned")%>' runat="server" />
                                                            <asp:CheckBox ID="ChkQty" runat="server" /></span>
                                                        </td>
                                                        <td><%# Eval("StyleCSID")%> </td>
                                                        <td><%# Eval("ColumnNo")%> </td>
                                                        <td>
                                                            <asp:TextBox ID="txtName" class="form-control" MaxLength="50" Text='<%# Eval("SettingName")%>' runat="server"></asp:TextBox>
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
                                    <asp:Button ID="btnSave" class="btn btn-success pull-right" Visible="false" runat="server" Text="Save" OnClick="btnSave_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
