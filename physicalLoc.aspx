<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="physicalLoc.aspx.cs" Inherits="physicalLoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Physical Location   
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Masters</li>
                <li class="active"><a  href="physicalLoc.aspx">Physical Location</a></li>
               
            </ol>
        </section>
     <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                 <div class="box box-primary" id="devCapone" runat="server" visible="false">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title"></h3>

                       <%-- <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                        </div>--%>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
               
                         <div class="row">

                            <div class="col-md-6">
                             <div class="form-group">
                                <label>Select Location Type</label>                              
                                <asp:DropDownList ID="LocationTypeID" runat="server" DataTextField="Name" DataValueField="LocationTypeID" CssClass="form-control">
                                  
                                </asp:DropDownList>
                            </div> </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Location Name</label>
                                
                                  <asp:TextBox ID="Location" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>

                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>Contact No.</label>
                                
                                  <asp:TextBox ID="Contact" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>

                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>Address</label>
                                
                                  <asp:TextBox ID="Address" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                    </div>
                    
                <!-- /.box-body -->
                </div>

                    <div class="box-body table-responsive ">
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                        <asp:Button ID="btnSave"  class="btn btn-success pull-right btn-round" runat="server" Text="Save" OnClick="btnSave_Click"/>
                        <asp:Button ID="btnCancel"  class="btn btn-danger pull-right btn-round" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>
                </div>
                <!-- /.box -->
            </div>
            </div>
     
        
<section class="content">
        <!-- /.box-header -->
        <div class="row">
            <div class="col-xs-12">
               
            <asp:Label ID="lblLevel" runat="server" Text="1" Visible="false"></asp:Label> 
            <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label> 
                
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title pull-right">
                            <asp:Button ID="btnadd" runat="server" Text="Add New Physical Loaction" class="btn btn-primary btn-sm" OnClick="btnadd_Click" /></h3>
                     <div id="divAddAlert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Succesfully Added</h5>

                                        </div>

                                        <div id="divUpdAlert" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Succesfully Updated</h5>
                                        </div>

                        <div id="divErrorAlert" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Transaction Failed</h5>

                                        </div>      
                    
                    </div>
                    <div class="box-body table-responsive">
                         
                        <table id="example111" class="table table-bordered table-striped dtSearch">
                            <thead>
                                <tr>
                                   <th>ID</th>
                                   <th>Location Type </th>
                                   <th>Location Name </th>
                                   <th>Contact </th>
                                    <th>Total Space </th>
                                   <th>Stock Count </th>
                                    <th>Available Space </th>
                                   <th>Add</th>
                                   <th>Action</th>
                               </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="GV" runat="server" OnItemDataBound="GV_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("LocationID")%>
                                            <asp:Label ID="LocationID" runat="server" Text='<%# Eval("LocationID").ToString() %>' Visible="false"></asp:Label> </td>
                                            <td><%# Eval("LocationTypeName")%></td> 
                                            <td><asp:Label ID="LocationName" runat="server" Text='<%# Eval("Location").ToString() %>' Visible="false"></asp:Label><%# Eval("Location")%></td> 
                                            <td><%# Eval("Contact")%></td>
                                            <td><asp:Label ID="totalSpace" runat="server" ></asp:Label></td>
                                            <td><%# Eval("stockCnt")%></td>
                                            <td><asp:Label ID="spaceAvailable" runat="server" ></asp:Label></td>
                                           <td> 
                                               <asp:LinkButton ID="btnAddSubLoc" runat="server" CssClass="btn btn-warning btn-sm" OnClick="btnAddSubLoc_Click" ><i class="fa fa-home"></i> Sublocation</asp:LinkButton>
                                               
                                           </td>
                                            <td>
                                                <asp:LinkButton ID="btnEditLoc" runat="server" CssClass="btn btn-info btn-sm" OnClick="btnEditLoc_Click" ><i class="fa fa-pencil"></i> Edit</asp:LinkButton>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                                <tfoot>
                            </tfoot>
                        </table>
                         
                    </div>
                    <!-- /.box-body -->
                </div>
   
                <!-- /.box -->
            </div>
            <!-- /.col -->
        </div>
   </section>
    </ContentTemplate>
                            </asp:UpdatePanel>
    </div>
</asp:Content>

