<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Uploadpayment_file.aspx.cs" Inherits="Uploadpayment_file" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="content-wrapper"> <section class="content-header">
        <h1>Upload Payment File 
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <%--<li>Report</li>--%>
            <li class="active">Payment File </li>
        </ol>
    </section>
     <asp:UpdatePanel ID="UpdatePanel20" runat="server">
            <ContentTemplate>
                                    <section>
                                           <div class="box">
            <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Upload Payment File</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
   
                         <div class="row">
                             <asp:Label ID="storelocation" runat="server" Visible="false" ></asp:Label>
                            <%-- <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                    </div>
                             <div id="divInsert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                    </div>
                             <div id="divUpdate" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                    </div>--%>
                           <table class="table table-bordered">
                               <tr>
                                   <td>
                                       <asp:DropDownList ID="virtualLocation" AutoPostBack="true" runat="server" CssClass="form-control select2" DataTextField="Location" DataValueField="LocationID"></asp:DropDownList>
                                   </td>
                                   <td>
                                       <asp:FileUpload ID="FileUpload1" runat="server"   />
                                   </td>
                                    <td>
                                    <asp:RadioButtonList ID="rbttype" class="form-control" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Font-Size="Large" Font-Bold="True" >

                                        <asp:ListItem Text="Excel" Value="excel"></asp:ListItem>

                                        <asp:ListItem Text="CSV" Value="csv"  style="margin-left: 15px;"></asp:ListItem>
                                     
                                    </asp:RadioButtonList>
                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1"
    ControlToValidate="rbttype" Text="Required" Font-Bold="True" ForeColor="red" 
    ValidationGroup="go">
</asp:RequiredFieldValidator>
                           
                                   </td>
                                   <td>
                                       <asp:LinkButton ID="btnuploadcsv" runat="server" CssClass="btn btn-primary pull-left" OnClick="btnuploadcsv_Click" ValidationGroup="go"><i class="fa fa-upload"></i> Upload</asp:LinkButton>
                                   </td>
                                  
                                   
                               </tr>
                               
                           </table>
                            
                                               </div>


                        <div runat="server" id="sales" visible="false">
                             <table class="table table-bordered table-striped table-hover dtSearch">
                                <thead>
                                <tr>
                                    <th width="15%">Order Date</th>
                                    <th>Sales Id</th>
                                    <th>Type</th>
                                    <th>Channel</th>
                                    <th>Sales Price</th>
                                    <%--//<th>Payment Status</th>--%>
                                    <th>Payable Amount</th>
                                    
                                    
                                   
                                </tr>
                                    </thead>
                                <tbody>
                                <asp:Repeater ID="rptsales" runat="server" OnItemDataBound="rptsales_ItemDataBound">
                                   <ItemTemplate>
                                       <tr>
                                           <td>
                                               <%--<asp:Label ID="id" runat="server" Visible="false" Text='<%# Eval("Pt_id").ToString() %>' ></asp:Label>--%>
                                               <%# Eval("order_date").ToString() %>
                                           </td>
                                           <td> <%# Eval("salesid").ToString() %></td>
                                           <td> <%# Eval("type").ToString() %></td>
                                           <td><asp:Label ID="lblloaction" runat="server" Visible="true" ></asp:Label></td>
                                           <td> <%# Eval("sp").ToString() %></td>
                                           <%--//<td> <%# Eval("Payment_Status").ToString().Equals("") ? "NA": Eval("Payment_Status").ToString() %></td>--%>
                                           <%--<td> <%# Eval("Payment_Type").ToString() %></td>--%>
                                           <td> <%# Eval("Payable_Amoun").ToString() %></td>

                                            
                                        

                                       </tr>
                                       </ItemTemplate>
                                </asp:Repeater>
                                </tbody>
                            </table>
                                <div class="box-footer btn-toolbar " style="margin-left: 0px;" >
                                  
                                    <asp:LinkButton ID="btnInsert" runat="server" CssClass="btn btn-sm btn-danger pull-right" OnClientClick="return confirm('Are you Sure You Want to Upload Duplicate Transaction ?');" OnClick="btnInsert_Click" ><i class="fa fa-upload" ></i>Upload </asp:LinkButton>
                                    <asp:LinkButton ID="btnexpoettoexcel" runat="server" CssClass="btn btn-sm btn-primary pull-right"   OnClick="btnexpoettoexcel_Click"><i class="fa  fa-file-excel-o" ></i>Export</asp:LinkButton>
                                   
                                    
                                    
                                                
                               </div>
                            </div>
                       
                                   </div>
                        </div>
                    </div>
                </div>     
                                    </section>
               



                   
</ContentTemplate>
         <Triggers>
             <asp:PostBackTrigger ControlID="btnuploadcsv" />
             <asp:PostBackTrigger ControlID="btnInsert" />
             <asp:PostBackTrigger ControlID="btnexpoettoexcel" />
         </Triggers>
         </asp:UpdatePanel>
            </div>
</asp:Content>

