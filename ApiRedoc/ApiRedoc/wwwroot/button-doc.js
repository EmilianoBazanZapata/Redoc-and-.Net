document.addEventListener("DOMContentLoaded", function () {
    setTimeout(function () {
        var button = document.createElement("button");
        button.innerText = "Show Documentation";
        button.className = "btn btn-primary";
        button.addEventListener("click", function () {
            window.location.href = "/api-docs/index.html";
        });

        var container = document.querySelector(".swagger-ui .topbar");
        container.appendChild(button);
    }, 5000);
});
