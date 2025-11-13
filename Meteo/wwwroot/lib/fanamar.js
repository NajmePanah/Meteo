document.addEventListener("DOMContentLoaded", function () {

    // ---------- مودال سامانه‌ها و پشتیبان‌ها ----------
    const relatedModal = document.getElementById('relatedSystemsModal');
    if (relatedModal) {
        relatedModal.addEventListener('show.bs.modal', function (event) {
            const categoryName = event.relatedTarget.getAttribute("data-category");
            document.getElementById("categoryTitleText").textContent = categoryName;

            const category = categories.find(c => c.CategoryTitle === categoryName);
            const container = document.getElementById("systems-container");
            container.innerHTML = "";

            if (!category) {
                container.innerHTML = "<p class='text-danger'>هیچ داده‌ای یافت نشد.</p>";
                return;
            }

            category.Systems.forEach(sys => {
                const htmlSupport = sys.SupportIds
                    .map(id => supports.find(s => s.Id === id))
                    .filter(x => x)
                    .map(s => `<div class="support-item"><span class="badge bg-label-info me-2 p-1"><i class="ti ti-user"></i></span><span class="support-name">${s.FullName}</span><span class="text-success">(${s.InternalPhone})</span></div>`)
                    .join("");

                const htmlDescription = sys.Description
                    ? `<div class="text-muted mt-2 small">
                        ${sys.Description.split("|").map(x => `<div>${x.trim()}</div>`).join("")}
                       </div>`
                    : "";

                container.innerHTML += `
                    <div class="list-group-item py-3">
                        <div class="system-row d-grid"
                             style="grid-template-columns: 1fr 250px; gap: 20px; align-items: start;">

                            <div class="system-info">
                                <span class="badge bg-label-warning me-2 p-1">
                                    <i class="ti ti-device-laptop"></i>
                                </span>
                                <a href="${sys.Url}" class="text-primary fw-bold text-decoration-none">
                                    ${sys.Title}
                                </a>
                                ${htmlDescription}
                            </div>
                            <div class="supports-box text-end">
                                ${htmlSupport}
                            </div>
                        </div>
                    </div>
                `;
            });
        });
    }

    // ---------- مودال فایل‌های آموزشی ----------
    const educationFilesData = {
        "طرح-جامع-مالیاتی": {
            title: "طرح جامع مالیاتی",
            categories: [
                {
                    name: "فایل‌های پیش‌فرض تیکت‌ها",
                    files: [
                        {
                            title: "فایل خام بارگذاری قبوض",
                            url: "/_content/Portal.Modules.Main/fanamarFiles/tarheJame/Ejraeeyat/راهنمای_کاربران_اجرائیات_مهندس_نوری.pdf"
                        }
                    ]
                }
            ]
        }
    };

    document.querySelectorAll('.open-education-files').forEach(el => {
        el.addEventListener('click', function () {
            const key = this.getAttribute('data-system');
            const data = educationFilesData[key];

            const modalTitle = document.getElementById('educationFilesTitle');
            const modalContent = document.getElementById('educationFilesContent');

            if (!data) {
                modalTitle.textContent = "فایل‌های آموزشی";
                modalContent.innerHTML = "<p class='text-muted'>هیچ فایلی یافت نشد.</p>";
                return;
            }

            modalTitle.textContent = data.title;

            let html = "";
            data.categories.forEach(cat => {
                html += `<h6 class="fw-bold mb-2">${cat.name}</h6><ul class="list-unstyled mb-4">`;
                cat.files.forEach(f => {
                    html += `<li class="mb-2"><a href="${f.url}" target="_blank">
                                <i class="ti ti-paperclip me-1"></i>${f.title}</a></li>`;
                });
                html += "</ul>";
            });

            modalContent.innerHTML = html;
        });
    });

});
