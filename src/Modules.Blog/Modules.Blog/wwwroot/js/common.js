const blobUrlToBase64=async e=>new Promise(t=>{fetch(e).then(e=>e.blob()).then(e=>{const o=new FileReader;o.addEventListener("load",()=>t(o.result)),o.readAsDataURL(e)})}),toast=(e,o=0,t=2e3)=>{const s=document.querySelector(".toast");var a;s&&((a=s.querySelector(".toast-body")).innerHTML=e,a.classList.add(o),s.classList.add("show"),setTimeout(function(){s.classList.remove("show")},t))};window.blobUrlToBase64=blobUrlToBase64,window.toast=toast;export{blobUrlToBase64,toast};