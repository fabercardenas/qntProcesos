<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileSMS.aspx.cs" Inherits="Web_TDC_FileSMS" %>
<% 
    Response.ContentType ="text/plain" ;
    Response.AddHeader("Content-Disposition", "attachment;filename=SMS.txt");
%> 
