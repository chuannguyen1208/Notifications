const blobToBase64=async s=>new Promise((o,e)=>{const t=new FileReader;t.onload=()=>o(t.result),t.error=o=>e(o),t.readAsDataURL(s)}),convertBlobURLToBase64=async o=>{o=await(await fetch(o)).blob();return await blobToBase64(o)},toast=(o,e=0,t=2e3)=>{const s=document.querySelector(".toast");var a;s&&((a=s.querySelector(".toast-body")).innerHTML=o,a.classList.add(e),s.classList.add("show"),setTimeout(function(){s.classList.remove("show")},t))};window.convertBlobURLToBase64=convertBlobURLToBase64,window.toast=toast;export{blobToBase64,convertBlobURLToBase64,toast};