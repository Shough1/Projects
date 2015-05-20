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
function AjaxGet( url, outData, callbackFnc )
{
	var jqXHR = $.get( url, outData);
	jqXHR.done( function( retData, retStatus){ // data response, and status of call
		console.log( retData + " : " + retStatus);
    if( callbackFnc == null ) return;
    callbackFnc( retData, retStatus ); // pass received data to callback
	});
	jqXHR.fail( function( retData, retStatus){ // data response, and status of call
		console.log( "Fail : " + retData + " : " + retStatus);
	});
	jqXHR.always( function( retData, retStatus){ // data response, and status of call
		console.log( "Always");
	});
}
// Issue Ajax Post with NO Json parsing
// - only to be used against responses 
//   of type application/javascript or application/json
function AjaxPost( url, outData, callbackFnc )
{
	var jqXHR = $.post(url, outData);
	jqXHR.done( function( retData, retStatus){ // data response, and status of call
    if( callbackFnc == null ) return;
		callbackFnc( retData, retStatus );
	});
	jqXHR.fail( function( retData, retStatus){ // data response, and status of call
		console.log( "Fail : " + retData + " : " + retStatus);
	});
	jqXHR.always( function( retData, retStatus){ // data response, and status of call
		console.log( "Always");
	});
}

// Issue Ajax Get with implicit/automatic Json parsing
// - only to be used against responses of type html/text
function AjaxGetJson( url, outData, callbackFnc )
{
	var jqXHR = $.getJSON( url, outData );
	jqXHR.done( function( retData, retStatus){ // data response, and status of call
    if( callbackFnc == null ) return;
    callbackFnc( outData, retStatus  );
	});
	jqXHR.fail( function( retData, retStatus){ // data response, and status of call
		console.log( "Fail : " + retData + " : " + retStatus);
	});
	jqXHR.always( function( retData, retStatus){ // data response, and status of call
		console.log( "Always");
	});
}

// Issue Ajax Get with explicit Json parsing
// - only to be used against responses of type html/text
function AjaxGetJsonEx( url, outData, callbackFnc )
{
	var jqXHR = $.get(url, outData);
	jqXHR.done( function( retData, retStatus){ // data response, and status of call
		var dat = $.parseJSON( retData );
    if( callbackFnc == null ) return;
		callbackFnc( dat, retStatus );
	});
	jqXHR.fail( function( retData, retStatus){ // data response, and status of call
		console.log( "Fail : " + retData + " : " + retStatus);
	});
	jqXHR.always( function( retData, retStatus){ // data response, and status of call
		console.log( "Always");
	});
}
// Issue Ajax Post with explicit Json parsing
// - only to be used against responses of type html/text
function AjaxPostJson( url, outData, callbackFnc )
{
	var jqXHR = $.post(url, outData);
	jqXHR.done( function( retData, retStatus){ // data response, and status of call
		var dat = $.parseJSON( retData );
    if( callbackFnc == null ) return;
		callbackFnc( dat, retStatus );
	});
	jqXHR.fail( function( retData, retStatus){ // data response, and status of call
		console.log( "Fail : " + retData + " : " + retStatus);
	});
	jqXHR.always( function( retData, retStatus){ // data response, and status of call
		console.log( "Always");
	});
}