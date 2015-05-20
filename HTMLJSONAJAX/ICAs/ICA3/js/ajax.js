// Sample pre-processing for these generic ajax calls
// var outData = {}; // make object! not array []
// outData["Name"] = "Ryan"; // property "Name" gets Ryan 
// outData["Grades"] = arrData;
//	
// var targetDiv = $("#target");
// var statusObj = $("#status");
//----AjaxFnc ( url, outData, callbackFnc )

// Issue Ajax Get 
// - only to be used against responses of type html/text
var auto = false;
var timerID = 0;
var width = 200;
$(document).ready(function() {

    $("#tags").click(function() {
        var search = {};
        search['action'] = 'search';
        search['filter'] = $("#txtFilter").val();
        AjaxPostJson('RTData.php', search, OptionOut);
    });

    $("#Refresh").click(function() {
        var selected = [];
        $(".slctItems option:selected").each(function() {
            selected.push($(this).val());
        });
        var values = {};
        values['action'] = 'refresh';
        values['values'] = selected;
        AjaxPostJson('RTData.php', values, TableOut);
    });

    $("#live").click(function() {
        if (auto != true)
        {
            var selected = [];
            $(".slctItems option:selected").each(function() {
                selected.push($(this).val());
            });
            var values = {};
            values['action'] = 'refresh';
            values['values'] = selected;
            timerID = window.setInterval(LiveUpdate, 1000);
            this.innerHTML = "Stop";
            auto = true;
        }
        else
        {
            window.clearInterval(timerID);
            $(this).html("Live Update");
            auto = false;
        }
    });
});

function LiveUpdate() {
    var selected = [];
    $(".slctItems option:selected").each(function() {
        selected.push($(this).val());
    });
    var values = {};
    values['action'] = 'refresh';
    values['values'] = selected;
    AjaxPostJson('RTData.php', values, TableOut)
}
function OptionOut(retData) {

    var options = "";
    for (var i = 0; i < retData.length; i++)
    {
        options += "<option value=" + retData[i]['tagID'] + ">" + retData[i]['tagID'] + "</option>";
    }
    $("#slctFilter").html(options);
}

function TableOut(retData) {
    var tbl = "";
    for (var i = 0; i < retData.length; i++)
    {
        var x = parseInt(retData[i]['value']);
        var low = parseInt(retData[i]['tagMin']);
        var hi = parseInt(retData[i]['tagMax']);
        var barNum = (x - low) / (hi - low);
        barNum = barNum * width;
        tbl += "<tr>";
        tbl += "<td>" + retData[i]['tagID'] + "</td><td>" + low + "</td><td>" + hi + "</td><td>" + x + "</td>"
        if (x < low)
        {
            barNum = (low - x) / (hi - low);
            if (barNum > 1)
            {
                barNum = barNum / 100;
                barNum = barNum * width;
            }
            else if (barNum < 0)
            {
                barNum = barNum * -width;
            }
            else
                barNum = barNum * width
            tbl += "<td style='Width: 200px'><div style='Width: " + width + "px; Height: 20px; Background-Color: Blue'><div style='Background-Color: Green; Height: 20px; Width: " + barNum + "px'></div></div></td>";
            console.log(retData[i]['tagID'] + " : Under-Range by " + (low - x))
        }
        else if (x > hi)
        {

            barNum = (x - hi) / (hi - low);
            if (barNum > 1)
            {
                barNum = barNum / 100;
                barNum = barNum * width;
            }
            else if (barNum < 0)
            {
                barNum = barNum * -width;
            }
            else
                barNum = barNum * width
            tbl += "<td style='Width: 200px'><div style='Width: " + width + "px; Height: 20px; Background-Color: Red'><div style='Background-Color: Green; Height: 20px; Width: " + barNum + "px'></div></div></td>";
            console.log(retData[i]['tagID'] + " : Over-Range: by " + (x - hi));
        }
        else
        {
            tbl += "<td style='Width: 200px'><div style='Width: " + width + "px; Height: 20px; Background-Color: Grey'><div style='Background-Color: Green; Height: 20px; Width: " + barNum + "px'></div></div></td>";
        }
        tbl += "</tr>";
    }
    $("#slctBdy").html(tbl);
}
function AjaxGet(url, outData, callbackFnc)
{
    var jqXHR = $.get(url, outData);
    jqXHR.done(function(retData, retStatus) { // data response, and status of call
        console.log(retData + " : " + retStatus);
        if (callbackFnc == null)
            return;
        callbackFnc(retData, retStatus); // pass received data to callback
    });
    jqXHR.fail(function(retData, retStatus) { // data response, and status of call
        console.log("Fail : " + retData + " : " + retStatus);
    });
    jqXHR.always(function(retData, retStatus) { // data response, and status of call
        console.log("Always");
    });
}
// Issue Ajax Post with NO Json parsing
// - only to be used against responses 
//   of type application/javascript or application/json
function AjaxPost(url, outData, callbackFnc)
{
    var jqXHR = $.post(url, outData);
    jqXHR.done(function(retData, retStatus) { // data response, and status of call
        if (callbackFnc == null)
            return;
        callbackFnc(retData, retStatus);
    });
    jqXHR.fail(function(retData, retStatus) { // data response, and status of call
        console.log("Fail : " + retData + " : " + retStatus);
    });
    jqXHR.always(function(retData, retStatus) { // data response, and status of call
        console.log("Always");
    });
}

// Issue Ajax Get with implicit/automatic Json parsing
// - only to be used against responses of type html/text
function AjaxGetJson(url, outData, callbackFnc)
{
    var jqXHR = $.getJSON(url, outData);
    jqXHR.done(function(retData, retStatus) { // data response, and status of call
        if (callbackFnc == null)
            return;
        callbackFnc(outData, retStatus);
    });
    jqXHR.fail(function(retData, retStatus) { // data response, and status of call
        console.log("Fail : " + retData + " : " + retStatus);
    });
    jqXHR.always(function(retData, retStatus) { // data response, and status of call
        console.log("Always");
    });
}

// Issue Ajax Get with explicit Json parsing
// - only to be used against responses of type html/text
function AjaxGetJsonEx(url, outData, callbackFnc)
{
    var jqXHR = $.get(url, outData);
    jqXHR.done(function(retData, retStatus) { // data response, and status of call
        var dat = $.parseJSON(retData);
        if (callbackFnc == null)
            return;
        callbackFnc(dat, retStatus);
    });
    jqXHR.fail(function(retData, retStatus) { // data response, and status of call
        console.log("Fail : " + retData + " : " + retStatus);
    });
    jqXHR.always(function(retData, retStatus) { // data response, and status of call
        console.log("Always");
    });
}
// Issue Ajax Post with explicit Json parsing
// - only to be used against responses of type html/text
function AjaxPostJson(url, outData, callbackFnc)
{
    var jqXHR = $.post(url, outData);
    jqXHR.done(function(retData, retStatus) { // data response, and status of call
        var dat = $.parseJSON(retData);
        if (callbackFnc === null)
            return;
        callbackFnc(dat, retStatus);
    });
    jqXHR.fail(function(retData, retStatus) { // data response, and status of call
        console.log("Fail : " + retData + " : " + retStatus);
    });
    jqXHR.always(function(retData, retStatus) { // data response, and status of call
        console.log("Always");
    });
}