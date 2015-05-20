Filename=ica03Demo/index.php

<?php
require_once 'inc/db.php'; // sqliconnect has completed, and $mysqli is our connection object

function TestQuery( $filter ) // $filter will be our title where contraint
{
  global $mysqli, $mysqli_response, $mysqli_status;// register the globals
  
  $filter = $mysqli->real_escape_string($filter);
  
  $query = "SELECT title_id, title, price ";
  $query.= " FROM titles";
  $query.= " WHERE title like '%$filter%'"; // wildcard for strings = %
  
  $outStr = "<ul>";
  if( $result = mysqliQuery($query))
  {// Made it here, query was good, we have data
    while( $row = $result->fetch_assoc()) // get ['title_id'] => title_id_value
    {// for each row, I will construct a list item
      $outStr .= "<li>{$row['title_id']} : {$row['title']} = {$row['price']}</li>";
    }
  }
  else
  {
    return "Query Error : $query";
  }
  $outStr .= "</ul>";
  return $outStr;
}

function TestNonQuery( $priceAdjust )
{
  global $mysqli, $mysqli_response, $mysqli_status;// register the globals

  $priceAdjust = $mysqli->real_escape_string($priceAdjust); // sanitize
  $query = "UPDATE `titles` SET price = price + $priceAdjust where price > 30";
  echo $query;
  if(($result = mysqliNonQuery($query)) == -1 ) // negative if bad
  {// bad !!!
      return "Yo, the query failed.";
  }
  return $result;
  
}

if( isset($_GET['action']) && $_GET['action'] == 'update' &&
    isset($_GET['price']) && is_numeric($_GET['price']))
{
  $updateResult = TestNonQuery($_GET['price']);
}

?>
<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title>ica03Demo</title>
		<link href='//fonts.googleapis.com/css?family=Ubuntu+Condensed|Ranchers&effect=3d' rel='stylesheet' type='text/css'>
		<style>
			body {  background-color: #bad; font-size:x-large; font-family:"Ubuntu Condensed", Verdana, sans-serif; }
			h1,h1 { font-family:"Ranchers", cursive;}
		</style>
	</head>
	<body>
		<div>
			<!-- Use session -->
			<h1 class='font-effect-3d'>ica03 mysqli Demo</h1>
		</div>
		<!-- Form -->
		<div>
			<br/>
			<div>
				<?php   // index.php?price=10&action=update
        $pr = 10;
        echo "<a href='index.php?price=". $pr . "&action=update'>Press ME for an UPdate</a>";
				// Part I - TestQuery check
        //echo TestQuery("COMPUTER");
        echo "<br/>rows affected : " . $updateResult. "<br/>";
        //echo "<br/>rows affected" . TestNonQuery(10) . "<br/>";
       
        echo TestQuery("COMPUTER");
				// Part II - Test non-query for say ? Update and returned row count
				?>
			</div>
		</div>
		</form>
	</body>
</html>

