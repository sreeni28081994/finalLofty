  
    function showDateTime() {
  var myDiv = document.getElementById("myDiv");

  var date = new Date();
  var dayList = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
  var monthNames = [
    "January",
    "February",
    "March",
    "April",
    "May",
    "June",
    "July",
    "August",
    "September",
    "October",
    "November",
    "December"
  ];
  var dayName = dayList[date.getDay()];
  var monthName = monthNames[date.getMonth()];
  var today = `${dayName}, ${monthName} ${date.getDate()}, ${date.getFullYear()}`;

  var hour = date.getHours();
  var min = date.getMinutes();
  var sec = date.getSeconds();

  var time = hour + ":" + min + ":" + sec;
  myDiv.innerText = `Today is  ${today}. Time is ${time}`;
}
setInterval(showDateTime, 1000);