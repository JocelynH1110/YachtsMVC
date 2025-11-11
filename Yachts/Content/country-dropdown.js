async function initDropdown() {
  const regionDropdown = document.querySelector("select#RegionDropdown");
  const countryDropdown = document.querySelector("select#CountryDropdown");
  const regionPlaceholderText = "請選擇地區";
  const countryPlaceholder = "請選擇國家";

  if (!regionDropdown || !countryDropdown) {
    return;
  }

  const countries = await fetch("/Content/countries.json").then((res) =>
    res.json(),
  );

  const regions = Array.from(
    new Set(countries.map(({ r }) => r).filter(Boolean)),
  ).sort();

  // remove all options in region dropdown
  regionDropdown.innerHTML = "";

  const regionPlaceholder = document.createElement("OPTION");
  regionPlaceholder.setAttribute("value", "");
  regionPlaceholder.innerText = regionPlaceholderText;
  regionDropdown.appendChild(regionPlaceholder);

  regions.forEach((r) => {
    const option = document.createElement("OPTION");
    option.innerText = r;
    regionDropdown.appendChild(option);
  });

  const presetRegion = regionDropdown.getAttribute("data-value");
  if (presetRegion && regions.includes(presetRegion)) {
    regionDropdown.value = presetRegion;
    handleRegionChange(presetRegion);
  }

  const presetCountry = countryDropdown.getAttribute("data-value");
  if (presetCountry && countries.some((c) => c.i === presetCountry)) {
    countryDropdown.value = presetCountry;
  }

  function handleRegionChange(value) {
    countryDropdown.innerHTML = "";

    const placeholder = document.createElement("OPTION");
    placeholder.setAttribute("value", "");
    placeholder.innerHTML = countryPlaceholder;
    countryDropdown.appendChild(placeholder);

    const options = countries
      .filter(({ r }) => r === value)
      .map(({ i }) => {
        const label = new Intl.DisplayNames("en", { type: "region" }).of(i);
        return { value: i, label };
      })
      .sort((a, b) => a.label.localeCompare(b.label));

    for (const { label, value } of options) {
      const option = document.createElement("OPTION");
      option.setAttribute("value", value);
      option.innerText = label;
      countryDropdown.appendChild(option);
    }
  }

  regionDropdown.addEventListener("change", (e) => {
    handleRegionChange(e.currentTarget.value);
  });
}

initDropdown();
