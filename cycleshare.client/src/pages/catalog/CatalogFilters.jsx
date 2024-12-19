import {
    Box,
    Checkbox,
    FormControl,
    FormControlLabel,
    FormGroup,
    InputLabel, MenuItem,
    Select,
    Slider,
    Typography
} from "@mui/material";
import FilterAltOutlinedIcon from "@mui/icons-material/FilterAltOutlined";
import debounce from "lodash/debounce";

const CATEGORIES = ["Steel", "Aluminium", "Carbon"];
const CatalogFilters = ({searchParams, onFilterChange}) => {
    const categories = new Set(searchParams.getAll('materials'));
    const priceRangeV = [searchParams.get('minPrice') || 0, searchParams.get('maxPrice') || 1000];
    const [priceRange, setPriceRange] = React.useState(priceRangeV);
    const [stateDebounceCallHttpRequest] = React.useState(() =>
        debounce(onFilterChange, 300, {
            leading: false,
            trailing: true
        })
    );
    // console.log('initial categories', [...categories], searchParams.getAll('categories'), categories.has('Steel'));
    const handleCategoryChange = (key, value) => {
        if (value) {
            categories.add(key);
        } else {
            categories.delete(key);
        }
        onFilterChange('materials', [...categories])
    };

    const handlePriceChange = (value) => {
        setPriceRange(value);
        stateDebounceCallHttpRequest([['minPrice', value[0]], ['maxPrice', value[1]]]);
    };

    return (
        <Box sx={{p: 2, border: "1px solid #ddd", borderRadius: 1}}>
            <Typography variant="h6" gutterBottom>
                <FilterAltOutlinedIcon/> Filters
            </Typography>

            <Typography variant="subtitle1" gutterBottom>
                Categories
            </Typography>
            <FormGroup>
                {CATEGORIES.map(category => (
                    <FormControlLabel
                        key={category}
                        control={
                            <Checkbox
                                checked={categories.has(category)}
                                onChange={(event) => handleCategoryChange(category, event.target.checked)}
                            />
                        }
                        label={category}
                    />
                ))}
            </FormGroup>

            <Typography variant="subtitle1" gutterBottom sx={{mt: 2}}>
                Price Range
            </Typography>
            <Slider
                value={priceRange}
                onChange={(event, value) => handlePriceChange(value)}
                valueLabelDisplay="auto"
                min={20}
                max={70}
            />

            <FormControl fullWidth sx={{mt: 2}}>
                <InputLabel>Minimum Rating</InputLabel>
                <Select
                    value={searchParams.get('minRating') || 0}
                    label="Minimum Rating"
                    onChange={(event) => onFilterChange('minRating', Number(event.target.value))}
                    variant={'outlined'}
                >
                    <MenuItem value={0}>All</MenuItem>
                    {[1, 2, 3, 4, 5].map(rating => (
                        <MenuItem key={rating} value={rating}>
                            {rating}+ Stars
                        </MenuItem>
                    ))}
                </Select>
            </FormControl>

            <FormControl fullWidth sx={{mt: 2}}>
                <InputLabel>Sort By</InputLabel>
                <Select
                    value={searchParams.get('sortBy') || 'name'}
                    label="Sort By"
                    onChange={(event) => onFilterChange('sortBy', event.target.value)}
                    variant={'outlined'}>
                    <MenuItem value="name">Name</MenuItem>
                    <MenuItem value="createdDate">Date</MenuItem>
                    <MenuItem value="rating">Rating</MenuItem>
                </Select>
            </FormControl>

            <FormControl fullWidth sx={{mt: 2}}>
                <InputLabel>Sort Order</InputLabel>
                <Select
                    value={searchParams.get('order') || 'asc'}
                    label="Sort Order"
                    onChange={(event) => onFilterChange('order', event.target.value)}
                    variant={'outlined'}
                >
                    <MenuItem value="asc">Ascending</MenuItem>
                    <MenuItem value="desc">Descending</MenuItem>
                </Select>
            </FormControl>
        </Box>
    );
};

export default CatalogFilters;
