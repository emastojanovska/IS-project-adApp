debugger;
var currentUser = await _UserManager.GetUserAsync(User);
var image = currentUser.Image.ImageSrc;
console.log(currentUser);
document.getElementById("avatar").src = image;