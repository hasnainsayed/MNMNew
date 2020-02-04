<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Size.aspx.cs" Inherits="Size" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Size
            </h1>
            <ol class="breadcrumb">
                <li><a href="index.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Master</li>
                <li class="active">Size</li>
            </ol>
        </section>

        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
            <ContentTemplate>
                <!-- Form  -->
                <div class="box box-primary" id="ctfrmDet" runat="server" visible="false">

                    <div class="box-body">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Select Item Category</label>
                                <%--<asp:RequiredFieldValidator ControlToValidate="ddlItemCategory" ID="selectproj"
                                    ValidationGroup="grp" ErrorMessage="Please select project" ForeColor="Red"
                                    InitialValue="0" runat="server" Display="Dynamic" />--%>

                                <asp:DropDownList ID="ddlItemCategory" runat="server"  CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Size Name1</label>
                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" ID="reqno" ErrorMessage="Please enter name" ControlToValidate="txtSize1" Display="Dynamic" />
                                <asp:TextBox ID="txtSize1" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                          <div class="col-md-4">
                            <div class="form-group">
                                <label>Size Name2</label>
                               <%-- <asp:RequiredFieldValidator runat="server" ForeColor="Red" ID="RequiredFieldValidator1" ErrorMessage="Please enter name" ControlToValidate="txtHSN" Display="Dynamic" />--%>
                                <asp:TextBox ID="txtSize2" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div> <div class="col-md-4">
                            <div class="form-group">
                                <label>Size Name3</label>
                               <%-- <asp:RequiredFieldValidator runat="server" ForeColor="Red" ID="RequiredFieldValidator2" ErrorMessage="Please enter name" ControlToValidate="txtHSN" Display="Dynamic" />--%>
                                <asp:TextBox ID="txtSize3" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div> <div class="col-md-4">
                            <div class="form-group">
                                <label>Size Name4</label>
                                <%--<asp:RequiredFieldValidator runat="server" ForeColor="Red" ID="RequiredFieldValidator3" ErrorMessage="Please enter name" ControlToValidate="txtHSN" Display="Dynamic" />--%>
                                <asp:TextBox ID="txtSize4" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                          <div class="col-md-12">
                                <div class="table-responsive">
                                    <table class="table table-bordered">
                                        <tr><td colspan="2">
                                            <asp:LinkButton CausesValidation="False" ID="addBrands" runat="server" CssClass="btn btn-sm btn-warning" OnClick="addBrands_Click"><i class="fa fa-plus"></i> Add More</asp:LinkButton></td></tr>
                                        <tr>
                                            <th>Brand</th>
                                            <th>Length</th>
                                        </tr>
                                        
                                            <asp:Repeater ID="rpt_Length" runat="server" OnItemDataBound="rpt_Length_ItemDataBound">
                                                 <ItemTemplate><tr>
                                                    <td>
                                                        <asp:DropDownList ID="brandId" runat="server" CssClass="form-control" DataTextField="C1Name" DataValueField="Col1ID"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="pId" runat="server" Text='<%# Eval("pId").ToString() %>' Visible="false" CssClass="form-control"></asp:TextBox>
                                                        <asp:TextBox ID="lengths" runat="server" Text='<%# Eval("lengths").ToString() %>' CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    
                                                </tr> </ItemTemplate>
                                            </asp:Repeater>
                                       
                                    </table>
                                </div>
                          </div>

                    </div>
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                       <asp:Button ID="btnSave" Visible="false" class="btn btn-success pull-right" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnUpdate" Visible="false" class="btn btn-warning pull-right" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnCancel" class="btn btn-danger pull-right" runat="server" Text="Back" OnClick="btnCancel_Click" CausesValidation="False" />
                    </div>
                </div>

                <section class="content">
                    <div class="row">
                        <div class="col-xs-12">

                            <div class="box">
                                <div class="box-header" >
                                    <h3 class="box-title" style="float:right;">
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
                                    <table class="table table-bordered table-striped dtSearch">
                                        <thead>
                                            <tr>
                                                <asp:Label ID="hdnID" runat="server" visible="false"/>
                                                <th>ID</th>
                                                <th>Item Category</th>
                                                <th>Size 1</th>
                                                 <th>Size 2</th>
                                                 <th>Size 3</th>
                                                 <th>Size 4</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater ID="GV" runat="server" OnItemCommand="rptr_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("SizeID")%> </td>
                                                        <td><%# Eval("ItemCategory")%> </td>
                                                        <td><%# Eval("Size1")%> </td>
                                                         <td><%# Eval("Size2")%> </td>
                                                         <td><%# Eval("Size3")%> </td>
                                                         <td><%# Eval("Size4")%> </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="edit"
                                                                ImageUrl="images/edit.png" CommandArgument='<%# Eval("SizeID") %>' />
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
</asp:Content>
