<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="todaysDeal.aspx.cs" Inherits="todaysDeal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="content-wrapper">
        <section class="content-header">
            <h1>Today's Deal Product</h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Website</li>
                <li class="active"><a  href="todaysDeal.aspx">Today's Deal</a></li>
               
            </ol>
        </section>
   
<section class="content">
        <!-- /.box-header -->
        <div class="row">
            <div class="col-xs-12">
               
            
                
                <div class="box">
                  
                    <div class="box-body table-responsive">
                        <div id="divAddAlert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Updated Succesfully</h5>

                                    </div>

                                    <div id="divUpdAlert" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Update Failed</h5>

                                    </div>

                       
                           <div class="col-md-12"> <div class="form-group">
                                <div class="col-md-8">
                                    <label>Style Code</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="styleCode"
        ValidationGroup="addCode" runat="server" ErrorMessage="Style Code is required." forecolor="Red" /><br />
                                     <asp:TextBox ID="styleCode" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                           <div class="col-md-4">
                            <asp:LinkButton runat="server" ValidationGroup="addCode" ID="addCode" class="btn bg-purple btn-round margin btn-sm" OnClick="addCode_Click">Add Code</asp:LinkButton>
                              </div> </div>  </div>  
                       
                        <div class="col-md-12">
                          <div class="form-group"> 
                           <div class="col-md-8"><label>File Upload</label>
                                           <asp:FileUpload ID="FileUpload1" runat="server" />
                            </div> 
                            <div class="col-md-4">
                                           <asp:Button ID="uploadExcel" runat="server" Text="Upload Excel" CssClass="btn btn-sm btn-danger" OnClick="uploadExcel_Click"/>
                           </div>  
                        </div> 
                         </div></div>
                     
                                    
                             <table class="table table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>Style Code : <asp:Label runat="server" id="totalBarcodes" style="color:red"></asp:Label></th>   
                                            <th>Title</th>
                                            <th>Remove</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%--<asp:Repeater ID="rpt_Style" runat="server" OnItemDataBound="rpt_Style_ItemDataBound">
                                                <ItemTemplate>
                                                <tr>
                                                    <td> 
                                                        <asp:Label runat="server" ID="styleId" Text='<%# Eval("styleId").ToString() %>' Visible="false"></asp:Label>
                                                        <asp:DropDownList ID="style_drop" CssClass="form-control select2" runat="server" DataTextField="Title" DataValueField="StyleID" AutoPostBack="true"></asp:DropDownList>
                                                    </td>
                                                   
                                                </tr>
                                       </ItemTemplate></asp:Repeater>--%>
                                        <asp:Repeater ID="rpt_Style" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><asp:Label runat="server" ID="StyleCode" Text='<%# Eval("StyleCode").ToString() %>'></asp:Label>
                                                        <asp:Label runat="server" ID="styleId" Text='<%# Eval("styleId").ToString() %>' Visible="false"></asp:Label></td>
                                                    <td><asp:Label runat="server" ID="Title" Text='<%# Eval("Title").ToString() %>'></asp:Label></td>
                                                    <td><asp:LinkButton runat="server" OnClientClick="return confirm('Do You Want to Delete?');" ID="delStyle" OnClick="delStyle_Click"><i class="fa fa-trash"></i></asp:LinkButton></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </tbody>
                                </table>

             

                        <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                        <asp:Button ID="btnSave"  class="btn btn-success pull-right btn-round" runat="server" Text="Save Today's Deal Product" OnClick="btnSave_Click" />
                        
                    </div>
                         
                    </div>
                    <!-- /.box-body -->
                </div>
   
                <!-- /.box -->
            </div>
         
   </section>
   
            </div>
</asp:Content>

